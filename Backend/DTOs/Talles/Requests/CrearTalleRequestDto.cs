using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.DTOs.Talles.Requests
{
    public class CrearTalleRequestDto
    {
        public string Nombre { get; set; } = string.Empty;
        public TipoTalle TipoTalle { get; set; }
    }
}