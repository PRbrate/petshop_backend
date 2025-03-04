using PetShop.Core.Base.Interfaces;
using PetShop.Domain.Entities;

namespace Appointment.Data.Repositories.Interfaces
{
    public interface IAppointmentsRepository : IRepositoryBase<Appointments>
    {
        Task AddTotValue(Appointments apt, double totValue);
        Task<Appointments> getAppointment(int appointmentId);
    }
}
