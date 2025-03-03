using Microsoft.EntityFrameworkCore;
using PetShop.Core.Base;
using PetShop.Data.Context;
using PetShop.Data.Repositories.Interfaces;
using PetShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.Repositories
{
    public class ServiceRepository : RepositoryBase<Service>, IServiceRepository
    {
        private readonly PetShopContext _context;

        public ServiceRepository(PetShopContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Service> GetByName(string name)
        {
            var services = await _context.Services.Where(x => x.Name.ToUpper() == name.ToUpper()).FirstOrDefaultAsync();

            return services;
        }
    }
}
