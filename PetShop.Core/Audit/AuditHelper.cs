using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;
using PetShop.Core.Audit.UserAgentHelper;

namespace PetShop.Core.Audit
{
    public partial class AuditHelper : IAuditHelper
    {
        private readonly IUser _user;
        private readonly ILogger<AuditHelper> _logger;
        private readonly IServiceProvider _service;
        private UserAgent _userAgent;

        public AuditHelper(IUser user, ILogger<AuditHelper> logger, IServiceProvider service)
        {
            _user = user;
            _logger = logger;
            _service = service;
        }


        private static string GetIp(HttpContext context)
        {
            return context.Connection.RemoteIpAddress?.ToString();
        }

        public AuditModel RegisterLog(HttpContext context, string module, string description = null, string model = null)
        {
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            try
            {
                context.Request.EnableBuffering();

                var username = _user.IsAuthenticated()
                    ? _user.GetUserEmail()
                    : "anonymous";

                var userId = _user.IsAuthenticated()
                    ? _user.GetUserId().ToString()
                    : "anonymous";

                if(description is null)
                {
                    var routeData = context.GetRouteData();

                    var controllerName = routeData.Values["controller"]?.ToString();
                    var actionName = routeData.Values["action"]?.ToString();

                    description = $"{controllerName}/{actionName}";
                }

                _userAgent = new UserAgent(context.Request.Headers["User-Agent"]);

                var modelJson = model;

                var log = new AuditDto()
                {
                    OccurrenceDate = DateTime.Now.AddHours(timeZone.BaseUtcOffset.Hours),
                    UserName = username,
                    UserId = userId,
                    System = module,
                    Ip = GetIp(context),
                    OperationalSystem = _userAgent.OS.Name + " " + _userAgent.OS.Version,
                    Browser = _userAgent.Browser.Name + " " + _userAgent.Browser.Version,
                    Mobile = MobileDetectorUtils.fBrowserIsMobile(context),
                    Action = context.Request.Path,
                    Description = description,
                    Model = modelJson ?? string.Empty
                };
                var audit = AuditDto.ViewModelToAuditoria(log);

                return audit;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return null;

        }


    }
}
