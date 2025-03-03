using Microsoft.EntityFrameworkCore;
using PetShop.Application.DTO;
using PetShop.Application.MappingsConfig;
using PetShop.Application.Services.Interfaces;
using PetShop.Core.Entities;
using PetShop.Data.Repositories;
using PetShop.Data.Repositories.Interfaces;
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Services
{
    public class ServicesServices : IServicesService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServicesServices(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<Response<ServiceDto>> CreateService(ServiceDto service)
        {
            var response = new Response<ServiceDto>();
            var services = AutoMapperServices.Map(service);
            services.Status = Status.Active;
            await _serviceRepository.Create(services);
            response.Success = true;
            return response;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var getService = await _serviceRepository.GetAsync(id);
            if (getService == null)
            {
                return false;
            }
            getService.Status = Status.Inactive;
            getService.UpdatedAt = DateTime.Now;
            _serviceRepository.Detached(getService);
            await _serviceRepository.Delete(getService);
            return true;
        }

        public async Task<Response<PaginationResult<ServiceDto>>> GetAll(int pageIndex, int pageSize)
        {
            var services = _serviceRepository.GetAllAsync();

            var items = await services
                .OrderBy(p => p.Name)
                .Where(p => p.Status == Status.Active)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var totalCount = items.Count();
            var list = new List<ServiceDto>();
            foreach (Service s in items)
            {
                list.Add(AutoMapperServices.Map(s));
            }
            var pag = new PaginationResult<ServiceDto>(list, totalCount, pageIndex, pageSize);
            var response = new Response<PaginationResult<ServiceDto>>(pag);
            return response;
        }

        public async Task<Response<ServiceDto>> GetById(int id)
        {
            var response = new Response<ServiceDto>();

            var service = await _serviceRepository.GetAsync(id);
            if (service == null || service.Status == Status.Inactive)
            {
                response.Success = false;
                response.Errors = "Service Not Found";
                return response;
            }
            response.Data = AutoMapperServices.Map(service);
            return response;
        }

        public async Task<Response<ServiceDto>> GetByName(string name)
        {
            var response = new Response<ServiceDto>();

            var service = await _serviceRepository.GetByName(name);
            if (service == null || service.Status == Status.Inactive)
            {
                response.Success = false;
                response.Errors = "Service Not Found";
                return response;
            }
            response.Data = AutoMapperServices.Map(service);
            return response;
        }

        public async Task<Response<ServiceDto>> UpdateService(int id, ServiceDto serviceDto)
        {
            var serviceByData = await _serviceRepository.GetAsync(id);
            var response = new Response<ServiceDto>();

            if (serviceByData == null || serviceByData.Status == Status.Inactive)
            {
                response.Success = false;
                response.Errors = "this Service not found";
                return response;
            }

            AutoMapperServices.Map(serviceByData, serviceDto);
            serviceByData.UpdatedAt = DateTime.Now;
            _serviceRepository.Detached(serviceByData);
            await _serviceRepository.Update(serviceByData);

            return response;
        }
    }
}
