using Microsoft.EntityFrameworkCore;
using PetShop.Application.DTO;
using PetShop.Application.MappingsConfig;
using PetShop.Application.Services.Interfaces;
using PetShop.Core.Entities;
using PetShop.Data.Repositories;
using PetShop.Data.Repositories.Interfaces;
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Enums;
using PetShop.Domain.Entities.Validations.Services;

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

        public async Task<bool> DeletePet(int id)
        {
            var getPet = await _petsRepository.GetAsync(id);
            if (getPet == null)
            {
                return false;
            }
            getPet.Status = Status.Inactive;
            getPet.UpdatedAt = DateTime.Now;
            _petsRepository.Detached(getPet);
            await _petsRepository.Delete(getPet);
            return true;
        }

        public async Task<Response<PetsDto>> GetPetById(int id)
        {
            var response = new Response<PetsDto>();

            var pets = await _petsRepository.GetAsync(id);
            if (pets == null || pets.Status == Status.Inactive)
            {
                response.Success = false;
                response.Errors = "Pet Not Found";
                return response;
            }
            response.Data = AutoMapperPets.Map(pets);
            return response;
        }

        public async Task<Response<PaginationResult<PetsDto>>> GetPetByUser(int id, int pageIndex, int pageSize)
        {
            var pets = _petsRepository.GetAllAsync();

            var items = await pets
                .OrderBy(p => p.FullName)
                .Where(p=> p.UserId == id && p.Status == Status.Active)
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

        public async Task<Response<PaginationResult<PetsDto>>> GetPets(int pageIndex, int pageSize)
        {
            var pets = _petsRepository.GetAllAsync();

            var items = await pets
                .OrderBy(p => p.FullName)
                .Where(p=> p.Status == Status.Active)
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
                .Where(x => x.Species == specie && x.Status == Status.Active)
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
                .Where(x => x.Gender == gender && x.Status == Status.Active)
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
                .Where(x => x.NeedAttention == attention && x.Status == Status.Active)
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
        public async Task<Response<PetsDto>> UpdatePet(PetsDto pet, int id)
        {
            var petByData = await _petsRepository.GetAsync(id);
            var response = new Response<PetsDto>();

            if (petByData == null || petByData.Status == Status.Inactive)
            {
                response.Success = false;
                response.Errors = "this Pet not found";
                return response;
            }

            AutoMapperPets.Map(petByData, pet);
            petByData.UpdatedAt = DateTime.Now;
            petByData.setAge();
            _petsRepository.Detached(petByData);
            await _petsRepository.Update(petByData);

            return response;
        }
    }

}
