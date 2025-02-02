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
    public class CompaniesRepository : RepositoryBase<Companies>, ICompaniesRepository
    {
        private readonly PetShopContext _context;

        public CompaniesRepository(PetShopContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Companies> GetByRegistrationNumberAsync(string registrationNumber)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(x => x.RegistrationNumber == registrationNumber);
            return company;
        }

        public async Task<Companies> GetByEmailAsync(string email)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(x => x.Email == email);
            return company;
        }


    }
}
