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
        Task<Response<Users>> CreateUser(UserDto users, string code);
        Task<Response<string>> Authenticate(string RegistrationNumber, string password);
        Task<Response<UserDataDto>> GetById(int id);
        Task<Response<List<UserDataDto>>> GetAll();
        Task<Response<List<UserDataDto>>> GetByPhoneNumber(string phoneNumber);
        Task<Response<UserDataDto>> GetByRegistrationNumber(string registrationNumber);
        Task<Response<UserDataDto>> GetByEmail(string email);
        Task<bool> DeleteUser(int id);
        Task<Response<UserDataDto>> UpdateUser(int id, UserDto userDto, string code);
        Task<Response<bool>> UpdateUser(int id, string password, string newPassword);


    }
}
