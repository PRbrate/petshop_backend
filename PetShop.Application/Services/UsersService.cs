using Microsoft.EntityFrameworkCore;
using PetShop.Application.DTO;
using PetShop.Application.MappingsConfig;
using PetShop.Application.Services.Interfaces;
using PetShop.Application.Services.OtherServices;
using PetShop.Core;
using PetShop.Core.Entities;
using PetShop.Data.Repositories;
using PetShop.Data.Repositories.Interfaces;
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Enums;
using PetShop.Domain.Entities.Validations.Services;

namespace PetShop.Application.Services
{
    public class UsersService : IUsersService
    {


        private readonly IUsersRepository _usersRepository;
        private readonly MemoryCacheService _memoryCacheService;


        public UsersService(IUsersRepository usersRepository, MemoryCacheService memoryCache)
        {
            _usersRepository = usersRepository;
            _memoryCacheService = memoryCache;
        }

        public async Task<Response<string>> Authenticate(string RegistrationNumber, string password)
        {
            var response = new Response<string>();
            Users userTester;
            int i;
            if (EmailValidatorService.VerifyEmail(RegistrationNumber))
            {
                userTester = await _usersRepository.GetByEmailAsync(RegistrationNumber);
                i = 1;
            }
            else if (UserNameValidatorService.ValidaUserName(RegistrationNumber))
            {
                userTester = await _usersRepository.GetUserByUserName(RegistrationNumber);
                i = 2;
            }
            else
            {
                RegistrationNumber = new string(RegistrationNumber.Where(char.IsDigit).ToArray());
                if (!CpfValidatorService.ValidateCPF((RegistrationNumber)))
                {
                    response.Success = false;
                    response.Errors = "Login with Email, Username and CPF is incorrect";
                    return response;
                }
                userTester = await _usersRepository.GetUserByRegistrationNumber(RegistrationNumber);
                i = 3;

            }

            if(userTester == null)
            {
                response.Success = false;
                response.Errors = "User Not found";
                return response;
            }

            if (!PasswordValidatorSerivce.VerifyPassword(password))
            {
                response.Success = false;
                response.Errors = "Invalid Password, The meaning must have 8 characters, with lowercase characters and special characters.";
                return response;
            }

            if (!PasswordCryptographyService.VerifyPassword(password, userTester.Password))
            {
                response.Success = false;
                response.Errors = "Invalid Password.";
                return response;
            }

            var userAuthenticate = await _usersRepository.AuthenticateUser(RegistrationNumber, userTester.Password, i);
            if (userAuthenticate == null)
            {
                response.Success = false;
                response.Errors = "unauthenticated user";
                return response;
            }

            var token = TokenService.GenerateToken(userAuthenticate);

            response.Data = token + "|" + userAuthenticate.UserName;
            return response;

        }
        public async Task<Response<Users>> CreateUser(UserDto usersDto, string code)
        {

            var response = new Response<Users>();
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
            if (!UserNameValidatorService.ValidaUserName((usersDto.UserName)))
            {
                response.Success = false;
                response.Errors = "UserName shouldn't contain spaces, characters(@, #, $, %, &, *, !) minlength = 5, maxlength = 30";
                return response;
            }
            if (await _usersRepository.GetUserByRegistrationNumber(usersDto.RegistrationNumber) != null)
            {
                response.Success = false;
                response.Errors = "There is already a user with that registration number";
                return response;
            }
            if (await _usersRepository.GetUserByUserName(usersDto.UserName) != null)
            {
                response.Success = false;
                response.Errors = "There is already a user with that UserName";
                return response;
            }
            if (await _usersRepository.GetByEmailAsync(usersDto.Email) != null)
            {
                response.Success = false;
                response.Errors = "There is already a user with that Email";
                return response;
            }

            string storyCode = _memoryCacheService.GetCode(usersDto.Email);

            if (storyCode == null)
            {
                response.Success = false;
                response.Errors = "Expired or invalid code!";
                return response;
            }
            if (storyCode != code)
            {
                response.Success = false;
                response.Errors = "Incorrect code! or expired";
                return response;
            }

            _memoryCacheService.RemoveCode(usersDto.Email);

            var user = AutoMapperUsers.ToUsers(usersDto);

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
            getUser.Status = Status.Inactive;
            getUser.UpdatedAt = DateTime.Now;
            _usersRepository.Detached(getUser);
            await _usersRepository.Delete(getUser);
            return true;
        }

        #region gets
        public async Task<Response<PaginationResult<UserDataDto>>> GetAll(int pageIndex, int pageSize)
        {
            var users = _usersRepository.GetAllAsync();

            var items = await users
                .OrderBy(p => p.FullName)
                .Where(p=> p.Status == Status.Active)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalCount = items.Count();
            var list = new List<UserDataDto>();

            foreach (Users u in items)
            {
                list.Add(AutoMapperUsers.ToUserDto(u));
            }
            var pag = new PaginationResult<UserDataDto>(list, totalCount, pageIndex, pageSize);
            var response = new Response<PaginationResult<UserDataDto>>(pag);

            return response;

        }

        public async Task<Response<UserDataDto>> GetByRegistrationNumber(string registrationNumber)
        {
            var response = new Response<UserDataDto>();
            registrationNumber = new string(registrationNumber.Where(char.IsDigit).ToArray());
            var user = await _usersRepository.GetUserByRegistrationNumber(registrationNumber);
            if (user == null || user.Status == Status.Inactive)
            {
                response.Success = false;
                response.Errors = "There is no such user with that RegistrationNumber on the database";
                return response;
            }
            response.Data = AutoMapperUsers.ToUserDto(user);
            return response;
        }

        public async Task<Response<UserDataDto>> GetByEmail(string email)
        {
            var response = new Response<UserDataDto>();
            var user = await _usersRepository.GetByEmailAsync(email);
            if (user == null || user.Status == Status.Inactive)
            {
                response.Success = false;
                response.Errors = "There is no such user with that Email on the database";
                return response;
            }
            response.Data = AutoMapperUsers.ToUserDto(user);
            return response;
        }
        public async Task<Response<PaginationResult<UserDataDto>>> GetByPhoneNumber(string phoneNumber, int pageIndex, int pageSize)
        {
            var users = _usersRepository.GetAllAsync();

            var items = await users
                .OrderBy(u => u.FullName)
                .Where(x => x.Phone == phoneNumber && x.Status == Status.Active)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalCount = items.Count();
            var list = new List<UserDataDto>();

            foreach (Users u in items)
            {
                list.Add(AutoMapperUsers.ToUserDto(u));
            }
            var pag = new PaginationResult<UserDataDto>(list, totalCount, pageIndex, pageSize);
            var response = new Response<PaginationResult<UserDataDto>>(pag);

            return response;
        }
        public async Task<Response<UserDataDto>> GetById(int id)
        {
            var response = new Response<UserDataDto>();
            var user = await _usersRepository.GetAsync(id);
            if (user == null || user.Status == Status.Inactive)
            {
                response.Success = false;
                response.Errors = "There is no such user with that ID on the database";
                return response;
            }
            response.Data = AutoMapperUsers.ToUserDto(user);
            return response;
        }
        #endregion

        #region update
        public async Task<Response<UserDataDto>> UpdateUser(int id, UserDto userDto, string code)
        {
            var userByIdData = await _usersRepository.GetAsync(id);
            var response = new Response<UserDataDto>();

            if (userByIdData == null || userByIdData.Status == Status.Inactive)
            {
                response.Success = false;
                response.Errors = "this User not found";
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

            string storyCode = _memoryCacheService.GetCode(userDto.Email);

            if (storyCode == null)
            {
                response.Success = false;
                response.Errors = "Expired or invalid code!";
                return response;
            }
            if (storyCode != code)
            {
                response.Success = false;
                response.Errors = "Incorrect code! or expired";
                return response;
            }
            _memoryCacheService.RemoveCode(userDto.Email);

            var user = AutoMapperUsers.ToUsers(userDto);
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

        public async Task<Response<bool>> UpdateUser(int id, string password, string newPassword)
        {
            var passwordUserData = _usersRepository.GetPasswordById(id);
            var response = new Response<bool>();

            if (string.IsNullOrEmpty(passwordUserData))
            {
                response.Success = false;
                response.Errors = "User Not Found";
                return response;
            }

            if (!PasswordValidatorSerivce.VerifyPassword(password) && !PasswordValidatorSerivce.VerifyPassword(newPassword))
            {
                response.Success = false;
                response.Errors = "Invalid Password, The meaning must have 8 characters, with lowercase characters and special characters.";
                return response;
            }
            if (!PasswordCryptographyService.VerifyPassword(password, passwordUserData))
            {
                response.Success = false;
                response.Errors = "password does not match the one registered in our system";
                return response;
            }

            newPassword = PasswordCryptographyService.Cryptography(newPassword);
            _usersRepository.UpdatePasswordUser(id, newPassword);
            response.Data = true;
            return response;
        }
        #endregion
    }
}
