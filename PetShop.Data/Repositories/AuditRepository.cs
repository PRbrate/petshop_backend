using Microsoft.EntityFrameworkCore;
using PetShop.Core.Audit;
using PetShop.Data.Context;
using PetShop.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly PetShopContext _context;

        public AuditRepository(PetShopContext context)
        {
            _context = context;
        }

        public async Task<List<AuditModel>> GetList()
        {
            var query = _context.Audit.AsQueryable();


            return await query.ToListAsync();
        }

        public async Task<bool> RegistrarLog(AuditModel audit)
        {
            await _context.Audit.AddAsync(audit);
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose() =>
       _context?.Dispose();
    }
}
