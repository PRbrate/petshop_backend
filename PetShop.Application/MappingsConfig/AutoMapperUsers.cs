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
        public static Users ToUsers(this UserDto usersDto) => new Users
        {

            FullName = usersDto.FullName,
            RegistrationNumber = new string(usersDto.RegistrationNumber.Where(char.IsDigit).ToArray()),            
            Email = usersDto.Email,
            Password = usersDto.Password,
            Phone = new string(usersDto.Phone.Where(char.IsDigit).ToArray()),
            PostalCode = new string(usersDto.PostalCode.Where(char.IsDigit).ToArray()),
            State = usersDto.State,
            Country = "Brazil",
            Address = usersDto.Address,
            City = usersDto.City
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
           (user.UserId, user.FullName, user.RegistrationNumber, 
            user.Email, user.Phone, user.PostalCode,
            user.State, user.City, user.Country
           );
    }
}
