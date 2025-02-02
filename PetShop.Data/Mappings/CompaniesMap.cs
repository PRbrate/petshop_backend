using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Enums;

namespace PetShop.Data.Mappings
{
    public class CompaniesMap : IEntityTypeConfiguration<Companies>
    {
        public void Configure(EntityTypeBuilder<Companies> builder)
        {
            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.CompanyName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.RegistrationNumber)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(18);

            builder.Property(p => p.Email)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(255);

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(11);

            builder.Property(p => p.PostalCode)
                .HasMaxLength(12);

            builder.Property(p => p.Status)
             .HasConversion(
                v => v.ToString(),
                v => (Status)Enum.Parse(typeof(Status), v)
                );
        }
    }
}
