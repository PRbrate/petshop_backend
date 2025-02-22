using PetShop.Core.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.Repositories.Interfaces
{
    public interface IAuditRepository
    {
        Task<bool> RegistrarLog(AuditModel auditoria);
        Task<List<AuditModel>> GetList();
    }
}
