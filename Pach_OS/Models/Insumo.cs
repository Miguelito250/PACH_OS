using System;
using System.Collections.Generic;

namespace Pach_OS.Models
{
    public partial class Insumo
    {
        public Insumo()
        {
            DetallesCompras = new HashSet<DetallesCompra>();
            ProductosInsumos = new HashSet<ProductosInsumo>();
            Proveedores = new HashSet<Proveedore>();
        }

        public int IdInsumos { get; set; }
        public string? NomInsumo { get; set; }
        public int? CantInsumo { get; set; }
        public int? ProveedoresId { get; set; }
        public string? TiempoLlegado { get; set; }

        public virtual ICollection<DetallesCompra> DetallesCompras { get; set; }
        public virtual ICollection<ProductosInsumo> ProductosInsumos { get; set; }
        public virtual ICollection<Proveedore> Proveedores { get; set; }
    }
}
