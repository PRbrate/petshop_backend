using Appointment.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task AddTotValue(Appointments apt, double totValue)
        {

            // Attaches the object to the context
            _context.Entry(apt).State = EntityState.Modified;

            // Modify only the desired field
            apt.UpdatedAt = DateTime.Now.ToUniversalTime();
            apt.TotalPrice = totValue;

            _context.Entry(apt).Property(u => u.TotalPrice).IsModified = true;
            _context.Entry(apt).Property(u => u.UpdatedAt).IsModified = true;

            await _context.SaveChangesAsync();
        }
    }
}
