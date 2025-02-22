using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Core.Extensions.Security
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public IEnumerable<Claim> GetClaimsIdentity() => _accessor.HttpContext.User.Claims;

        public string GetLocalIpAddress() => _accessor.HttpContext?.Connection.LocalIpAddress?.ToString();

        public string GetRemoteIpAddress() => _accessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

        public string GetUserEmail() => IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : "";

        public string GetUserId() => IsAuthenticated() ? _accessor.HttpContext.User.GetUserId() : default;

        public bool IsAuthenticated() => _accessor.HttpContext.User.Identity.IsAuthenticated;

        public bool IsInRole(string role) => _accessor.HttpContext.User.IsInRole(role);
    }
}
