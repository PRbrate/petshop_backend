using PetShop.Application.DTO;
using PetShop.Domain.Entities.Enums;

namespace Appointment.Application.DTO
{
    public record AppointmentsReturnDto(int appointmentId, int userId, int petId, DateOnly appointmentDate
        , StatusAppointments statusAppointments, double totalPrice, PaymentStatus paymentStatus
        , PaymentMethod paymentMethod, string notes, List<ServiceDto> services);
}
