using Microsoft.AspNetCore.Http;

namespace PetShop.Core.Audit
{
    public interface IAuditHelper
    {
        AuditModel RegisterLog(HttpContext context, string module, string description = null, string model = null);

    }
}
