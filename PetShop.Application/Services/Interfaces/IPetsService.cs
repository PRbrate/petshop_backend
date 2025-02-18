using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShop.Application.DTO;
using PetShop.Core.Entities;
using PetShop.Domain.Entities;

namespace PetShop.Application.Services.Interfaces
{
    public interface IPetsService
    {
        Task<Response<PetsDto>> CreatePet(PetsDto pet);
        Task<Response<PetsDto>> UpdatePet(PetsDto pet, int id);
        Task<Response<PetsDto>> DeletePet(int id);
        Task<Response<List<PetsDto>>> GetPets();
        Task<Response<PetsDto>> GetPetById(int id);
        Task<Response<List<PetsDto>>> GetPetByUser(int id);
    }
}
