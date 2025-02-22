using PetShop.Core.Audit;

namespace PetShop.Application.Services.Interfaces
{
    public interface IAuditService
    {
        Task<bool> RegisterLog(AuditModel audit);
        Task<List<AuditModel>> GetList();
    }
}
