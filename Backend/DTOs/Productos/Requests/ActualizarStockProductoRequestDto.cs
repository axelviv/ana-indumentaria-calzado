using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs.Productos.Requests
{
    public class ActualizarStockProductoRequestDto
    {
        public int ProductoTalleId { get; set; }
        public int Stock { get; set; }
    }
}