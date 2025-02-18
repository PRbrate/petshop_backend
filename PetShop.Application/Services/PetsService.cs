using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PetShop.Application.DTO;
using PetShop.Application.MappingsConfig;
using PetShop.Application.Services.Interfaces;
using PetShop.Core.Entities;
using PetShop.Data.Repositories;
using PetShop.Data.Repositories.Interfaces;

namespace PetShop.Application.Services
{
    public class PetsService : IPetsService
    {
        private readonly IPetsRepository _petsRepository;

        public PetsService(IPetsRepository petsRepository)
        {
            _petsRepository = petsRepository;
        }

        public async Task<Response<PetsDto>> CreatePet(PetsDto pet)
        {
            var response = new Response<PetsDto>();
            var pets = AutoMapperPets.Map(pet);
            pets.setAge();
            await _petsRepository.Create(pets);
            response.Success = true;
            return response;
        }

        public Task<Response<PetsDto>> DeletePet(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<PetsDto>> GetPetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<PetsDto>>> GetPetByUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<PetsDto>>> GetPets()
        {
            throw new NotImplementedException();
        }

        public Task<Response<PetsDto>> UpdatePet(PetsDto pet, int id)
        {
            throw new NotImplementedException();
        }
    }
}
