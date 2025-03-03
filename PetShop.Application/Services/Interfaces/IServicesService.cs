using PetShop.Application.DTO;
using PetShop.Core.Entities;
using PetShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Services.Interfaces
{
    public interface IServicesService
    {
        Task<Response<ServiceDto>> CreateService(ServiceDto service);
        Task<Response<ServiceDto>> GetById(int id);
        Task<Response<ServiceDto>> GetByName(string name);
        Task<Response<PaginationResult<ServiceDto>>> GetAll(int pageIndex, int pageSize);
        Task<bool> DeleteUser(int id);
        Task<Response<ServiceDto>> UpdateService(int id, ServiceDto serviceDto);
    }
}
