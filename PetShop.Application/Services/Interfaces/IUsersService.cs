using PetShop.Application.DTO;
using PetShop.Application.Filters;
using PetShop.Core.Entities;
using PetShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Services.Interfaces
{
    public interface IUsersService
    {
        Task<InternalResponse<Users>> CreateUser(Users users);

        Task<InternalResponse<string>> Authenticate(string RegistrationNumber, string password);

    }
}
