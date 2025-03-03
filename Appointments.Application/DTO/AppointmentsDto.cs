using PetShop.Domain.Entities.Enums;

namespace Appointment.Application.DTO
{
    public record AppointmentsDto(int appointmentId, int userId, int petId, DateOnly appointmentDate
        , StatusAppointments statusAppointments, float totalPrice, PaymentStatus paymentStatus
        , PaymentMethod paymentMethod, string notes);
}
