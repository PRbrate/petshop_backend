using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PetShop.Application.DTO;
using PetShop.Core.Entities;
using PetShop.Domain.Entities;

namespace PetShop.Application.MappingsConfig
{
    public static class AutoMapperUsers
    {
        public static Users ToUsers(this UserDto usersDto, Response<CepResponse> response) => new Users
        {
            FullName = usersDto.FullName,
            RegistrationNumber = usersDto.RegistrationNumber,
            CompanyId = usersDto.CompanyId,
            Email = usersDto.Email,
            Password = usersDto.Password,
            Phone = usersDto.Phone,
            PostalCode = response.Data.cep,
            State = response.Data.state,
            Address = response.Data.street,
            Country = "Brazil",
            City = response.Data.city
        };
        public static Users ToUsers(this UserDataDto usersDto, Response<CepResponse> response) => new Users
        {
            FullName = usersDto.FullName,
            RegistrationNumber = usersDto.RegistrationNumber,
            CompanyId = usersDto.CompanyId,
            Email = usersDto.Email,
            Phone = usersDto.Phone,
            PostalCode = response.Data.cep,
            State = response.Data.state,
            Address = response.Data.street,
            Country = "Brazil",
            City = response.Data.city
        };
        public static Users UsersAddress(this Response<CepResponse> response) => new Users
        {
            PostalCode = response.Data.cep,
            State = response.Data.state,
            Address = response.Data.street,
            Country = "Brazil",
            City = response.Data.city
        };

        public static UserDataDto ToUserDto(this Users user) => new
           (user.FullName, user.RegistrationNumber, user.CompanyId,
            user.Email, user.Phone, user.PostalCode,
            user.State, user.City, user.Country,
            user.Address
           );
    }
}
