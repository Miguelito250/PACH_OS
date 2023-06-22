using System;
using System.Collections.Generic;

namespace Pach_OS.Models
{
    public partial class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }
        public int? VentaId { get; set; }
        public int? CantVendida { get; set; }
        public int? ProductosId { get; set; }
        public int? TotalPrecio { get; set; }
        public int? Precio { get; set; }


        public virtual Producto? Productos { get; set; }
        public virtual Venta? Venta { get; set; }
    }
}
