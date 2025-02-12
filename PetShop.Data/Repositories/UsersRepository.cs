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
    public class UsersRepository : RepositoryBase<Users>, IUsersRepository
    {
        private readonly PetShopContext _Context;

        public UsersRepository(PetShopContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<Users> AuthenticateUser(string registrationNumber, string password)
        {
            var userAuth = await _Context.Users.FirstOrDefaultAsync(x => x.RegistrationNumber == registrationNumber && x.Password == password);

            return userAuth;
        }

        public async Task<Users> GetByEmailAsync(string email)
        {
            var company = await _Context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return company;
        }

        public async Task<Users> GetUserByRegistrationNumber(string registrationNumber)
        {
            var usersRegistratioNumber = await _Context.Users.FirstOrDefaultAsync(x => x.RegistrationNumber == registrationNumber);

            return usersRegistratioNumber;
        }

        public async Task<List<Users>> GetByPhoneNumber(string phoneNumber)
        {
            var usersPhoneNumber = await _Context.Users.Where(u=> u.Phone == phoneNumber).ToListAsync();
            return usersPhoneNumber;
        }
    }
}
