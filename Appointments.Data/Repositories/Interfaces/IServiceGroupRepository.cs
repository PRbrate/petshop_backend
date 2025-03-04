using PetShop.Core.Base.Interfaces;
using PetShop.Domain.Entities;

namespace Appointment.Data.Repositories.Interfaces
{
    public interface IServiceGroupRepository : IRepositoryBase<ServiceGroup>
    {
        Task AddServiceGroup(List<ServiceGroup> sg);
    }
}
