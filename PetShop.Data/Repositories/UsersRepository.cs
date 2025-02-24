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
        private readonly PetShopContext _context;

        public UsersRepository(PetShopContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Users> AuthenticateUser(string registrationNumber, string password, int i)
        {
            Users userAuth;
            switch (i)
            {
                case 1:
                    userAuth = await _context.Users.FirstOrDefaultAsync(x => x.Email == registrationNumber && x.Password == password);
                    break;
                case 2:
                    userAuth = await _context.Users.FirstOrDefaultAsync(x => x.UserName == registrationNumber && x.Password == password);
                    break;
                case 3:
                    userAuth = await _context.Users.FirstOrDefaultAsync(x => x.RegistrationNumber == registrationNumber && x.Password == password);
                    break;
                default:
                    userAuth = new Users();
                    break;
            }
            return userAuth;
        }

        public async Task<Users> GetByEmailAsync(string email)
        {
            var company = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return company;
        }

        public async Task<Users> GetUserByRegistrationNumber(string registrationNumber)
        {
            var usersRegistratioNumber = await _context.Users.FirstOrDefaultAsync(x => x.RegistrationNumber == registrationNumber);

            return usersRegistratioNumber;
        }

        public async Task<Users> GetUserByUserName(string userName)
        {
            var uUsersName = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);

            return uUsersName;
        }

        public async Task<List<Users>> GetByPhoneNumber(string phoneNumber)
        {
            var usersPhoneNumber = await _context.Users.Where(u => u.Phone == phoneNumber).ToListAsync();
            return usersPhoneNumber;
        }

        public string GetPasswordById(int userId)
        {
            return _context.Users
                .Where(u => u.UserId == userId)
                .Select(u => u.Password) //Select only the required field
                .FirstOrDefault();
        }

        public void UpdatePasswordUser(int userId, string password)
        {

            var user = new Users { UserId = userId };

            // Attaches the object to the context
            _context.Users.Attach(user);

            // Modify only the desired field
            user.Password = password;
            user.UpdatedAt = DateTime.Now.ToUniversalTime();

            _context.Entry(user).Property(u => u.Password).IsModified = true;
            _context.Entry(user).Property(u => u.UpdatedAt).IsModified = true;

            _context.SaveChanges();
        }
    }

}

