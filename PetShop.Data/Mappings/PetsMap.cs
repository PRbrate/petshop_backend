using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.Mappings
{
    public class PetsMap : IEntityTypeConfiguration<Pets>
    {
        public void Configure(EntityTypeBuilder<Pets> builder)
        {
            builder.Property(p => p.FullName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.Species)
             .HasConversion(
                v => v.ToString(),
                v => (Species)Enum.Parse(typeof(Species), v)
                );

            builder.Property(p => p.Gender)
             .HasConversion(
                v => v.ToString(),
                v => (Gender)Enum.Parse(typeof(Gender), v)
                );
        }
    }
}
