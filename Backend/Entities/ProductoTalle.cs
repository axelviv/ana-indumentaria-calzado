using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities
{
    public class ProductoTalle
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;
        public int TalleId { get; set; }
        public Talle Talle { get; set; } = null!;
        public int Stock { get; set; }
    }
}