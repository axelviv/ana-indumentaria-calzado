using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Configurations
{
    public class TalleConfiguration : IEntityTypeConfiguration<Talle>
    {
        public void Configure(EntityTypeBuilder<Talle> builder)
        {
            builder.ToTable("Talles");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Nombre)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(t => t.TipoTalle)
                .IsRequired();

            builder.Property(t => t.Activo)
                .HasDefaultValue(true);

            builder.HasIndex(t => new { t.Nombre, t.TipoTalle })
                .IsUnique();

            builder.HasMany(t => t.ProductoTalles)
                .WithOne(pt => pt.Talle)
                .HasForeignKey(pt => pt.TalleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}