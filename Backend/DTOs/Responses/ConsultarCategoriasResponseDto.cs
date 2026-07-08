using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs.Responses
{
    public class ConsultarCategoriasResponseDto
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public List<CategoriaResponseDto> Categorias { get; set; } = new();
    }
}