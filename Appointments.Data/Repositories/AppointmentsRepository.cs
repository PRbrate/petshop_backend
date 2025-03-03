using Appointment.Data.Repositories.Interfaces;
using PetShop.Core.Base;
using PetShop.Data.Context;
using PetShop.Domain.Entities;

namespace Appointment.Data.Repositories
{
    public class AppointmentsRepository : RepositoryBase<Appointments>, IAppointmentsRepository
    {
        private readonly PetShopContext _context;

        public AppointmentsRepository(PetShopContext context) : base(context)
        {
            _context = context;
        }
    }
}
