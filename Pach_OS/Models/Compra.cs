using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pach_OS.Models
{
    public partial class Compra
    {
        public Compra()
        {
            DetallesCompras = new HashSet<DetallesCompra>();
        }

        [Key]
        public int IdCompras { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCompra { get; set; }
        public int? Total { get; set; }
        public int? IdProveedor { get; set; }

        public virtual Proveedore? IdProveedorNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<DetallesCompra> DetallesCompras { get; set; }
    }
}
