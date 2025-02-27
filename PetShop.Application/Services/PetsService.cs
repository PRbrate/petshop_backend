using Microsoft.EntityFrameworkCore;
using PetShop.Application.DTO;
using PetShop.Application.MappingsConfig;
using PetShop.Application.Services.Interfaces;
using PetShop.Core.Entities;
using PetShop.Data.Repositories.Interfaces;
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<Response<PaginationResult<PetsDto>>> GetPets(int pageIndex, int pageSize)
        {
            var pets = _petsRepository.GetAllAsync();

            var items = await pets
                .OrderBy(p => p.FullName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var totalCount = items.Count();
            var list = new List<PetsDto>();
            foreach (Pets p in items)
            {
                list.Add(AutoMapperPets.Map(p));
            }
            var pag = new PaginationResult<PetsDto>(list, totalCount, pageIndex, pageSize);
            var response = new Response<PaginationResult<PetsDto>>(pag);
            return response;
        }

        public async Task<Response<PaginationResult<PetsDto>>> GetPetsBySpecie(Species specie, int pageIndex, int pageSize)
        {            
            var pets = _petsRepository.GetAllAsync();

            var items = await pets
                .OrderBy(p => p.FullName)
                .Where(x => x.Species == specie)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var totalCount = items.Count();
            var list = new List<PetsDto>();

            foreach (Pets p in items)
            {
                list.Add(AutoMapperPets.Map(p));
            }
            var pag = new PaginationResult<PetsDto>(list, totalCount, pageIndex, pageSize);
            var response = new Response<PaginationResult<PetsDto>>(pag);
            
            return response;
        }

        public async Task<Response<PaginationResult<PetsDto>>> GetPetsByGender(Gender gender, int pageIndex, int pageSize)
        {
            var pets = _petsRepository.GetAllAsync();

            var items = await pets
                .OrderBy(p => p.FullName)
                .Where(x => x.Gender == gender)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var totalCount = items.Count();
            var list = new List<PetsDto>();

            foreach (Pets p in items)
            {
                list.Add(AutoMapperPets.Map(p));
            }
            var pag = new PaginationResult<PetsDto>(list, totalCount, pageIndex, pageSize);
            var response = new Response<PaginationResult<PetsDto>>(pag);

            return response;
        }


        public async Task<Response<PaginationResult<PetsDto>>> GetNeedAttention(bool attention, int pageIndex, int pageSize)
        {
            var pets = _petsRepository.GetAllAsync();

            var items = await pets
                .OrderBy(p => p.FullName)
                .Where(x => x.NeedAttention == attention)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var totalCount = items.Count();
            var list = new List<PetsDto>();

            foreach (Pets p in items)
            {
                list.Add(AutoMapperPets.Map(p));
            }
            var pag = new PaginationResult<PetsDto>(list, totalCount, pageIndex, pageSize);
            var response = new Response<PaginationResult<PetsDto>>(pag);

            return response;

        }
        public Task<Response<PetsDto>> UpdatePet(PetsDto pet, int id)
        {
            throw new NotImplementedException();
        }
    }
}
