using PetShop.Application.DTO;
using PetShop.Application.Filters;
using PetShop.Application.MappingsConfig;
using PetShop.Application.Services.Interfaces;
using PetShop.Core;
using PetShop.Core.Entities;
using PetShop.Data.Repositories;
using PetShop.Data.Repositories.Interfaces;
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Enums;
using PetShop.Domain.Entities.Validations.Services;
using PetShop.Facade.Interfaces;
using PetShop.Facade.Services;
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
        private readonly IBrasilApiHttpService _brasilApiHttpService;

        public UsersService(IUsersRepository usersRepository, IBrasilApiHttpService brasilApiHttpService)
        {
            _usersRepository = usersRepository;
            _brasilApiHttpService = brasilApiHttpService;
        }

        public async Task<InternalResponse<string>> Authenticate(string RegistrationNumber, string password)
        {
            var response = new InternalResponse<string>();
            RegistrationNumber = new string(RegistrationNumber.Where(char.IsDigit).ToArray());
            if (!CpfValidatorService.ValidateCPF((RegistrationNumber)))
            {
                response.Success = false;
                response.Errors = "Invalid CPF";
                return response;
            }

            if (!PasswordValidatorSerivce.VerifyPassword(password))
            {
                response.Success = false;
                response.Errors = "Invalid Password, The meaning must have 8 characters, with lowercase characters and special characters.";
                return response;
            }

            var userTester = await _usersRepository.GetUserByRegistrationNumber(RegistrationNumber);

            if (!PasswordCryptographyService.VerifyPassword(password, userTester.Password))
            {
                response.Success = false;
                response.Errors = "Invalid Password.";
                return response;
            }


            var userAuthenticate = await _usersRepository.AuthenticateUser(RegistrationNumber, userTester.Password);
            if (userAuthenticate == null)
            {
                response.Success = false;
                response.Errors = "User Not found";
                return response;
            }

            var token = TokenService.GenerateToken(userAuthenticate);

            response.Data = token;
            return response;

        }

        public async Task<InternalResponse<Users>> CreateUser(UserDto usersDto)
        {

            var response = new InternalResponse<Users>();
            if (!PasswordValidatorSerivce.VerifyPassword(usersDto.Password))
            {
                response.Success = false;
                response.Errors = "Invalid Password, The meaning must have 8 characters, with lowercase characters and special characters.";
                return response;
            }
            if (!CpfValidatorService.ValidateCPF((usersDto.RegistrationNumber)))
            {
                response.Success = false;
                response.Errors = "Invalid CPF";
                return response;
            }
            if (!EmailValidatorService.VerifyEmail(usersDto.Email))
            {
                response.Success = false;
                response.Errors = "Invalid Email";
                return response;
            }
            if (!PhoneNumberValidatorService.VerifyPhoneNumber(usersDto.Phone))
            {
                response.Success = false;
                response.Errors = "Invalid phone number";
                return response;
            }
            if (await _usersRepository.GetUserByRegistrationNumber(usersDto.RegistrationNumber) != null)
            {
                response.Success = false;
                response.Errors = "There is already a user with that registration number";
                return response;
            }

            var address = await _brasilApiHttpService.GetCep(usersDto.Cep);
            var user = AutoMapperUsers.ToUsers(usersDto, address);

            user.RegistrationNumber = new string(user.RegistrationNumber.Where(char.IsDigit).ToArray());
            user.Phone = new string(user.Phone.Where(char.IsDigit).ToArray());
            user.PostalCode = new string(user.PostalCode.Where(char.IsDigit).ToArray());

            user.Password = PasswordCryptographyService.Cryptography(user.Password);
            user.UserType = UserType.Costumer;
            await _usersRepository.Create(user);

            response.Success = true;
            return response;

        }

        public async Task<bool> DeleteUser(int id)
        {
            var getUser = await _usersRepository.GetAsync(id);
            if (getUser == null)
            {
                return false;
            }
            await _usersRepository.Delete(getUser);
            return true;
        }
        public async Task<InternalResponse<List<UserDataDto>>> GetAll()
        {
            var list = new List<UserDataDto>();
            var response = new InternalResponse<List<UserDataDto>>();
            var user = await _usersRepository.GetAllAsync();
            if (user == null)
            {
                response.Success = false;
                response.Errors = "There is no such data on the database";
                return response;
            }

            foreach (Users u in user)
            {
                list.Add(AutoMapperUsers.ToUserDto(u));
            }
            response.Data = list;
            return response;
        }

        public async Task<InternalResponse<List<UserDataDto>>> GetAllByCompanyId(int companyId)
        {
            var list = new List<UserDataDto>();
            var response = new InternalResponse<List<UserDataDto>>();
            var user = await _usersRepository.GetAllByCompanyId(companyId);
            if (user == null)
            {
                response.Success = false;
                response.Errors = "There is no such data on the database";
                return response;
            }

            foreach (Users u in user)
            {
                list.Add(AutoMapperUsers.ToUserDto(u));
            }
            response.Data = list;
            return response;
        }

        public async Task<InternalResponse<UserDataDto>> GetById(int id)
        {
            var response = new InternalResponse<UserDataDto>();
            var user = await _usersRepository.GetAsync(id);
            if (user == null)
            {
                response.Success = false;
                response.Errors = "There is no such user with that ID on the database";
                return response;
            }
            response.Data = AutoMapperUsers.ToUserDto(user);
            return response;
        }

        public async Task<InternalResponse<UserDataDto>> UpdateUser(int id, UserDto userDto)
        {
            var userByIdData = await _usersRepository.GetAsync(id);
            var response = new InternalResponse<UserDataDto>();

            if (userByIdData == null)
            {
                response.Success = false;
                response.Errors = "this Company not found";
                return response;
            }
            if (userDto.Email != null)
            {
                if (!EmailValidatorService.VerifyEmail(userDto.Email))
                {
                    response.Success = false;
                    response.Errors = "Invalid Email";
                    return response;
                }
            }
            if (userDto.Phone != null)
            {
                if (!PhoneNumberValidatorService.VerifyPhoneNumber(userDto.Phone))
                {
                    response.Success = false;
                    response.Errors = "Invalid PhoneNumber";
                    return response;
                }
            }

            if (await _usersRepository.GetByEmailAsync(userDto.Email) != null)
            {
                response.Success = false;
                response.Errors = "this Email already exists";
                return response;
            }
            var address = await _brasilApiHttpService.GetCep(userByIdData.PostalCode);

            if (userDto.Cep != null)
            {
                address = await _brasilApiHttpService.GetCep(userDto.Cep);
            }

            var user = AutoMapperUsers.ToUsers(userDto, address);
            userByIdData = InsertUser(userByIdData, user);
            _usersRepository.Detached(userByIdData);
            userByIdData.UpdatedAt = DateTime.Now;
            await _usersRepository.Update(userByIdData);
            return response;
        }

        public Users InsertUser(Users user, Users userDto)
        {
            if (!string.IsNullOrEmpty(userDto.FullName))
            {
                user.FullName = userDto.FullName;
            }
            if (!string.IsNullOrEmpty(userDto.Email))
            {
                user.Email = userDto.Email;
            }
            if (!string.IsNullOrEmpty(userDto.Phone))
            {
                user.Phone = userDto.Phone;
            }
            if (!string.IsNullOrEmpty(userDto.PostalCode))
            {
                user.PostalCode = userDto.PostalCode;
            }
            return user;

        }
    }
}
