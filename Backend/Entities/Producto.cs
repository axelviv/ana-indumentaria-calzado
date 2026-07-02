using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.Entities
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public Genero Genero { get; set; }
        public bool Activo { get; set; } = true;
        public string? RutaImagen { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;
        public DateTime FechaAlta { get; set; } = DateTime.Now;

        // Propiedad de navegación
        public ICollection<ProductoTalle> Talles { get; set; } = new List<ProductoTalle>();
    }
}