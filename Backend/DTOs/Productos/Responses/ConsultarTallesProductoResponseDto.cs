using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs.Productos.Responses
{
    public class ConsultarTallesProductoResponseDto
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public List<ProductoTalleResponseDto> Talles { get; set; } = new();
    }
}