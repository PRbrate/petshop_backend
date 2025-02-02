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

        public async Task<Users> AuthenticateUser(string RegistrationNumber, string Password)
        {
            var usuarioAuth = await _Context.Users.FirstOrDefaultAsync(x => x.RegistrationNumber == RegistrationNumber && x.Password == Password);

            return usuarioAuth;
        }
    }
}
