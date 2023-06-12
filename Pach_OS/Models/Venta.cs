using System;
using System.Collections.Generic;

namespace Pach_OS.Models
{
    public partial class Venta
    {
        public Venta()
        {
            DetalleVenta = new HashSet<DetalleVenta>();
        }

        public int IdVentas { get; set; }
        public int? UsuarioId { get; set; }
        public DateTime? FechaVenta { get; set; }
        public int? TotalVenta { get; set; }
        public string? TipoPago { get; set; }
        public int? Pago { get; set; }
        public int? PagoDomicilio { get; set; }

        public virtual Usuario? Usuario { get; set; }
        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
    }
}
