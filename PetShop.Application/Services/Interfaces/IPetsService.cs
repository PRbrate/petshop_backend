using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShop.Application.DTO;
using PetShop.Core.Entities;
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Enums;

namespace PetShop.Application.Services.Interfaces
{
    public interface IPetsService
    {
        Task<Response<PetsDto>> CreatePet(PetsDto pet);
        Task<Response<PetsDto>> UpdatePet(PetsDto pet, int id);
        Task<bool> DeletePet(int id);
        Task<Response<PaginationResult<PetsDto>>> GetPets(int pageIndex, int pageSize);
        Task<Response<PetsDto>> GetPetById(int id);
        Task<Response<PaginationResult<PetsDto>>> GetPetByUser(int id, int pageIndex, int pageSize);
        Task<Response<PaginationResult<PetsDto>>> GetPetsBySpecie(Species specie, int pageIndex, int pageSize);
        Task<Response<PaginationResult<PetsDto>>> GetPetsByGender(Gender gender, int pageIndex, int pageSize);
        Task<Response<PaginationResult<PetsDto>>> GetNeedAttention(bool attention, int pageIndex, int pageSize);
    }
}
