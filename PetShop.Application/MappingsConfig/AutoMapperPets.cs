using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShop.Application.DTO;
using PetShop.Domain.Entities;

namespace PetShop.Application.MappingsConfig
{
    public static class AutoMapperPets
    {
        public static Pets Map(this PetsDto petsDto) => new Pets
        {
            PetId = petsDto.petsId,
            UserId = petsDto.userId,
            FullName = petsDto.fullName,
            Species = petsDto.species,
            Breed = petsDto.breed,
            Age = petsDto.age,
            Birthdate = petsDto.birthDate.ToDateTime(new TimeOnly(0, 0)),
            Gender = petsDto.gender,
            NeedAttention = petsDto.needAttention
        };

        public static void Map(this Pets pet, PetsDto petsDto)
        {
            if (!string.IsNullOrWhiteSpace(petsDto.fullName) && petsDto.fullName != "string")
                pet.FullName = petsDto.fullName;
            if (petsDto.species != pet.Species)
                pet.Species = petsDto.species;
            if (!string.IsNullOrWhiteSpace(petsDto.breed) && petsDto.breed != "string")
                pet.Breed = petsDto.breed;
            if (petsDto.birthDate.ToString() != pet.Birthdate.Date.ToString())
                pet.Birthdate = petsDto.birthDate.ToDateTime(new TimeOnly(DateTime.Now.Hour));
            if (petsDto.gender != pet.Gender)
                pet.Gender = petsDto.gender;
        }
        public static PetsDto Map(this Pets pets) => new(pets.PetId, pets.UserId, pets.FullName, pets.Species,
            pets.Breed, pets.Age, DateOnly.FromDateTime(pets.Birthdate), pets.Gender, pets.NeedAttention);
    }
}
