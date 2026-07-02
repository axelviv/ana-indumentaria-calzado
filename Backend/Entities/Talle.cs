using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.Entities
{
    public class Talle
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public TipoTalle TipoTalle { get; set; }

        public bool Activo { get; set; } = true;

        // Propiedad de navegación
        public ICollection<ProductoTalle> ProductoTalles { get; set; } = new List<ProductoTalle>();
    }
}