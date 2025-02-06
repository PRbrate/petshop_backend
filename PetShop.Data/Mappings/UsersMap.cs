using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Enums;

namespace PetShop.Data.Mappings
{
    public class UsersMap : IEntityTypeConfiguration<Users>
    {

        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.Property(p => p.FullName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.RegistrationNumber)
                .HasMaxLength(11)
                .IsUnicode(true);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.Password)
             .IsRequired()
             .HasMaxLength(255);

            builder.Property(p => p.Phone)
             .IsRequired()
             .HasMaxLength(11);

            builder.Property(p => p.Status)
             .HasConversion(
                v => v.ToString(),
                v => (Status)Enum.Parse(typeof(Status), v)
                );

            builder.Property(p => p.UserType)
             .HasConversion(
                v => v.ToString(),
                v => (UserType)Enum.Parse(typeof(UserType), v)
                );


        }
    }
}
