using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Configurations
{
    public class ProductoTalleConfiguration : IEntityTypeConfiguration<ProductoTalle>
    {
        public void Configure(EntityTypeBuilder<ProductoTalle> builder)
        {
            builder.ToTable("ProductoTalles");

            builder.HasKey(pt => pt.Id);

            builder.Property(pt => pt.Stock)
                .IsRequired();

            builder.HasOne(pt => pt.Producto)
                .WithMany(p => p.Talles)
                .HasForeignKey(pt => pt.ProductoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pt => pt.Talle)
                .WithMany(t => t.ProductoTalles)
                .HasForeignKey(pt => pt.TalleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(pt => new { pt.ProductoId, pt.TalleId })
                .IsUnique();
        }
    }
}