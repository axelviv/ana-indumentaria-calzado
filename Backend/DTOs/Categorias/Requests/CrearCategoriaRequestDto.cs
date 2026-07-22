using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.DTOs.Categorias.Requests
{
    public class CrearCategoriaRequestDto
    {
        public string Nombre { get; set; } = string.Empty;
        public TipoTalle TipoTalle { get; set; }
    }
}