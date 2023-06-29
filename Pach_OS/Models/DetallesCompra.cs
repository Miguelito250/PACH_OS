using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pach_OS.Models
{
    public partial class DetallesCompra
    {
        [Key]
        public int IdDetallesCompras { get; set; }
        public int? ComprasId { get; set; }
        public int? InsumosId { get; set; }
        public int? PrecioInsumo { get; set; }
        public byte? Cantidad { get; set; }

        public virtual Compra? Compras { get; set; }
        public virtual Insumo? Insumos { get; set; }
    }
}
