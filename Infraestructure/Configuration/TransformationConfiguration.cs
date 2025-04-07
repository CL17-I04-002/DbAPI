using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Configuration
{
    public class TransformationConfiguration : IEntityTypeConfiguration<Transformation>
    {
        public void Configure(EntityTypeBuilder<Transformation> builder)
        {
            builder.ToTable(nameof(Transformation));

            builder.Property(t => t.Name)
                .HasMaxLength(25)
                .HasColumnType("varchar(25)")
                .IsRequired();

            builder.Property(t => t.Ki)
                .HasMaxLength(35)
                .HasColumnType("varchar(35)");
        }
    }
}
