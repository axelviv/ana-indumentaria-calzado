using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs.Talles.Responses
{
    public class ConsultarTallesResponseDto
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public List<TalleResponseDto> Talles { get; set; } = new();
    }
}