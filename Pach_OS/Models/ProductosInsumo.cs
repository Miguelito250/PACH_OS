using System;
using System.Collections.Generic;

namespace Pach_OS.Models
{
    public partial class ProductosInsumo
    {
        public int? ProductosId { get; set; }
        public int? InsumosId { get; set; }
        public int? CantInsumo { get; set; }
        public int Id { get; set; }

        public virtual Insumo? Insumos { get; set; }
        public virtual Producto? Productos { get; set; }
    }
}
