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
    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.ToTable(nameof(Character));

            builder.Property(c => c.Name)
                .HasMaxLength(25)
                .HasColumnType("varchar(25)")
                .IsRequired();

            builder.Property(c => c.Race)
                .HasMaxLength(25)
                .HasColumnType("varchar(25)");

            builder.Property(c => c.Gender)
                .HasMaxLength(20)
                .HasColumnType("varchar(20)");

            builder.Property(c => c.Ki)
                .HasMaxLength(35)
                .HasColumnType("varchar(35)");

            builder.Property(c => c.Description)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Affiliation)
                .HasMaxLength(35)
                .HasColumnType("varchar(35)");
            builder
            .HasMany(c => c.Transformations)
            .WithOne(t => t.Character)
            .HasForeignKey(t => t.CharacterId);
        }
    }
}
