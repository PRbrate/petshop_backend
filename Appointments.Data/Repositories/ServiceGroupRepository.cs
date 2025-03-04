using Appointment.Data.Repositories.Interfaces;
using PetShop.Core.Base;
using PetShop.Data.Context;
using PetShop.Domain.Entities;

namespace Appointment.Data.Repositories
{
    public class ServiceGroupRepository : RepositoryBase<ServiceGroup>, IServiceGroupRepository
    {
        private readonly PetShopContext _context;

        public ServiceGroupRepository(PetShopContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddServiceGroup(List<ServiceGroup> sg)
        {
            await _context.AddRangeAsync(sg);
            await _context.SaveChangesAsync();
        }
    }
}
