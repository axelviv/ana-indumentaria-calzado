using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Configurations
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Productos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Descripcion)
                .HasMaxLength(500);

            builder.Property(p => p.Precio)
                .HasPrecision(18, 2);

            builder.Property(p => p.RutaImagen)
                .HasMaxLength(255);

            builder.Property(p => p.Activo)
                .HasDefaultValue(true);

            builder.HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Talles)
                .WithOne(pt => pt.Producto)
                .HasForeignKey(pt => pt.ProductoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.FechaAlta)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }        
    }
}