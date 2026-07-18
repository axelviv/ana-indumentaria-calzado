using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.DTOs.Responses
{
    public class ProductoResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public Genero Genero { get; set; }
        public string? RutaImagen { get; set; }
        public int CategoriaId { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}