using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Enums;

namespace PetShop.Application.DTO
{
    public record PetsDto(int petsId, int userId, string fullName, Species species, string breed, int age, DateTime birthDate, Gender gender);

}