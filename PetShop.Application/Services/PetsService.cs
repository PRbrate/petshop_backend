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
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Enums;

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

        public async Task<Response<PetsDto>> GetPetById(int id)
        {
            var response = new Response<PetsDto>();

            var pets = await _petsRepository.GetAsync(id);
            if (pets == null)
            {
                response.Success = false;
                response.Errors = "Pet Not Found";
                return response;
            }
            response.Data = AutoMapperPets.Map(pets);
            return response;
        }

        public async Task<Response<List<PetsDto>>> GetPetByUser(int id)
        {
            var response = new Response<List<PetsDto>>();
            var list = new List<PetsDto>();
            var pets = await _petsRepository.GetByUserId(id);
            if (pets == null)
            {
                response.Success = false;
                response.Errors = "There is no such pets with this user";
                return response;
            }
            foreach (Pets p in pets)
            {
                list.Add(AutoMapperPets.Map(p));
            }

            response.Data = list;
            return response;
        }        

        public async Task<Response<List<PetsDto>>> GetPets()
        {
            var response = new Response<List<PetsDto>>();
            var list = new List<PetsDto>();

            var pets = await _petsRepository.GetAllAsync();
            if (pets == null)
            {
                response.Success = false;
                response.Errors = "There is no such data on the database";
                return response;
            }

            foreach (Pets p in pets)
            {
                list.Add(AutoMapperPets.Map(p));
            }
            response.Data = list;
            return response;
        }

        public async Task<Response<List<PetsDto>>> GetPetsBySpecie(Species specie)
        {
            var response = new Response<List<PetsDto>>();
            var list = new List<PetsDto>();

            var pets = await _petsRepository.GetBySpecie(specie);
            if (pets == null)
            {
                response.Success = false;
                response.Errors = "Pets Not Found";
                return response;
            }

            foreach (Pets p in pets)
            {
                list.Add(AutoMapperPets.Map(p));
            }
            response.Data = list;
            return response;
        }

        public async Task<Response<List<PetsDto>>> GetPetsByGender(Gender gender)
        {
            var response = new Response<List<PetsDto>>();
            var list = new List<PetsDto>();

            var pets = await _petsRepository.GetByGender(gender);

            if (pets == null)
            {
                response.Success = false;
                response.Errors = "Pets Not Found";
                return response;
            }

            foreach (Pets p in pets)
            {
                list.Add(AutoMapperPets.Map(p));
            }
            response.Data = list;
            return response;
        }
        public Task<Response<PetsDto>> UpdatePet(PetsDto pet, int id)
        {
            throw new NotImplementedException();
        }
    }
}
