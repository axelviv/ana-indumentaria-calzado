using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.DTOs.Productos.Requests
{
    public class ActualizarProductoRequestDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public Genero Genero { get; set; }
        public string? RutaImagen { get; set; }
        public int CategoriaId { get; set; }
    }
}