using PetShop.Application.DTO;
using PetShop.Domain.Entities;

namespace PetShop.Application.MappingsConfig
{
    public static class AutoMapperServices
    {
        public static Service Map(this ServiceDto serviceDto) => new Service
        {
            ServiceId = serviceDto.serviceId,
            Name = serviceDto.name,
            Description = serviceDto.descripton,
            Price = serviceDto.price,
            Duration = serviceDto.duration

        };

        public static void Map(this Service service, ServiceDto serviceDto)
        {
            if (!string.IsNullOrWhiteSpace(serviceDto.name) && serviceDto.name != "string")
                service.Name = serviceDto.name;
            if (!string.IsNullOrWhiteSpace(serviceDto.descripton) && serviceDto.descripton != "string")
                service.Description = serviceDto.descripton;
            if (!double.IsNaN(serviceDto.duration))
                service.Duration = serviceDto.duration;
            if (!double.IsNaN(serviceDto.price))
                service.Price = serviceDto.price;
        }

        public static ServiceDto Map(this Service service) => new(service.ServiceId, service.Name, service.Description, service.Price, service.Duration);
    }
}
