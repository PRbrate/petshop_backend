using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShop.Core.Audit;

namespace PetShop.Data.Mappings
{
    public class AuditModelMap : IEntityTypeConfiguration<AuditModel>
    {
        public void Configure(EntityTypeBuilder<AuditModel> builder)
        {
            builder.Property(p => p.Description)
            .HasMaxLength(500)
            .IsUnicode(false);

            builder.Property(p => p.Model)
            .HasMaxLength(1000)
            .IsUnicode(false);

        }
    }
}
