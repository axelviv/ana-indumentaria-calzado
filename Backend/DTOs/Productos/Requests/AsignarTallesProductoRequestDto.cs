using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs.Productos.Requests
{
    public class AsignarTallesProductoRequestDto
    {
        public int ProductoId { get; set; }
        public List<TalleStockDto> Talles { get; set; } = new();
    }
}