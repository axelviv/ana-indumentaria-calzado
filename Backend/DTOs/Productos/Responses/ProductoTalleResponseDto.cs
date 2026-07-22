using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs.Productos.Responses
{
    public class ProductoTalleResponseDto
    {
        public int ProductoTalleId { get; set; }
        public int TalleId { get; set; }
        public string Talle { get; set; } = string.Empty;
        public int Stock { get; set; }
    }
}