using Appointment.Application.DTO;
using PetShop.Application.DTO;
using PetShop.Application.MappingsConfig;
using PetShop.Domain.Entities;

namespace Appointment.Application.MappingsConfig
{
    public static class AutoMapperAppointments
    {
        public static Appointments Map(this AppointmentsDto aptDto) => new Appointments
        {
            AppointmentId = aptDto.appointmentId,
            UserId = aptDto.userId,
            PetId = aptDto.petId,
            AppointmentDate = aptDto.appointmentDate.ToDateTime(new TimeOnly(DateTime.Now.Hour)),
            StatusAppointment = aptDto.statusAppointments,
            TotalPrice = aptDto.totalPrice,
            PaymentMethod = aptDto.paymentMethod,
            PaymentStatus = aptDto.paymentStatus,
            Notes = aptDto.notes
        };

        public static AppointmentsDto Map(this Appointments apt) => new(apt.AppointmentId, apt.UserId,
            apt.PetId, DateOnly.FromDateTime(apt.AppointmentDate), apt.StatusAppointment, apt.TotalPrice,
            apt.PaymentStatus, apt.PaymentMethod, apt.Notes);

        public static AppointmentsReturnDto MapReturn(this Appointments apt) => new(apt.AppointmentId, apt.UserId,
            apt.PetId, DateOnly.FromDateTime(apt.AppointmentDate), apt.StatusAppointment, apt.TotalPrice,
            apt.PaymentStatus, apt.PaymentMethod, apt.Notes, apt.ServiceGroups.Select(sg => AutoMapperServices.Map(sg.Services)).ToList());
    }
}
