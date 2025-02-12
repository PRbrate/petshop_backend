using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Services.OtherServices;

namespace PetShop.Api.Controllers.V1
{
    [Route("api/mailSend")]
    [ApiController]
    public class MailSendController : ControllerBase
    {
        private readonly EmailService _emailService;
        private readonly MemoryCacheService _memoryCache;

        public MailSendController(EmailService emailService, MemoryCacheService memoryCache)
        {
            _emailService = emailService;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        public IActionResult SendMail(string email)
        {
            string code = CodeGenerateService.CodeGenerate();

            _emailService.SendMail(email, code);
            _memoryCache.StoryCode(email, code);

            return Ok("email sent successfully!");
        }

        [HttpPost("verify")]
        public IActionResult VerifyCodigo(string email, string code)
        {
            string storyCode = _memoryCache.GetCode(email);

            if (storyCode == null)
            {
                return BadRequest(new { message = "Expired or invalid code!" });
            }
            if (storyCode != code)
            {
                return BadRequest(new { message = "Incorrect code! or expired" });
            }
            _memoryCache.RemoveCode(email);

            return Ok(new { message = "Code verified successfully!" });
        }
    }
}
