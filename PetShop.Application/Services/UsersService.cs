using PetShop.Application.Services.Interfaces;
using PetShop.Core;
using PetShop.Core.Entities;
using PetShop.Data.Repositories.Interfaces;
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Validations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<InternalResponse<string>> Authenticate(string RegistrationNumber, string password)
        {
            var response = new InternalResponse<string>();
            if (!PasswordValidatorSerivce.VerifyPassword(password))
            {
                response.Success = false;
                response.Errors = "Invalid Password, The meaning must have 8 characters, with lowercase characters and special characters.";
                return response;
            }
            if (!CpfValidatorService.ValidateCPF((RegistrationNumber)))
            {
                response.Success = false;
                response.Errors = "Invalid CPF";
                return response;
            }

            var userAuthenticate = await _usersRepository.AuthenticateUser(RegistrationNumber, password);
            if (userAuthenticate == null) 
            {
                response.Success = false;
                response.Errors = "User Not found";
                return response;
            }
            //ganerate token authorization
            var token = TokenService.GenerateToken(userAuthenticate);

            response.Data = token;
            return response;

        }

        public async Task<InternalResponse<Users>> CreateUser(Users users)
        {
            var response = new InternalResponse<Users>();
            var user = await _usersRepository.Create(users);

            return response;
            
        }
    }
}
