using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Enums;

namespace Appointment.Data.Mappings
{
    public class AppointmentsMap : IEntityTypeConfiguration<Appointments>
    {
        public void Configure(EntityTypeBuilder<Appointments> builder)
        {

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Property(p => p.PetId)
                .IsRequired();

            builder.Property(p => p.PaymentMethod)
             .HasConversion(
                v => v.ToString(),
                v => (PaymentMethod)Enum.Parse(typeof(PaymentMethod), v)
                );

            builder.Property(p => p.PaymentStatus)
             .HasConversion(
                v => v.ToString(),
                v => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), v)
                );

            builder.Property(p => p.Status)
             .HasConversion(
                v => v.ToString(),
                v => (StatusAppointments)Enum.Parse(typeof(StatusAppointments), v)
                );
        }
    }
}
