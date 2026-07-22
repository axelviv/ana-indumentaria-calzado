using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.DTOs.Categorias.Responses
{
    public class CategoriaResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public TipoTalle TipoTalle { get; set; }
    }
}