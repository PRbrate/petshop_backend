using Microsoft.EntityFrameworkCore;
using PetShop.Core.Base.Interfaces;
using PetShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.Repositories.Interfaces
{
    public interface ICompaniesRepository : IRepositoryBase<Companies>
    {
        Task<Companies> GetByRegistrationNumberAsync(string registrationNumber);
        Task<Companies> GetByEmailAsync(string email);


    }
}
