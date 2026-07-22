using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs.Categorias.Responses
{
    public class EliminarCategoriaResponseDto
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}