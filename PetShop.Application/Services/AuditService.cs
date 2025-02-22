using PetShop.Application.Services.Interfaces;
using PetShop.Core.Audit;
using PetShop.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepository;

        public AuditService(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        public async Task<List<AuditModel>> GetList()
        {
            return await _auditRepository.GetList();
        }

        public async Task<bool> RegisterLog(AuditModel audit)
        {
            return await _auditRepository.RegistrarLog(audit);
        }
    }
}
