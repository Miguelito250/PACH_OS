using System;
using System.Collections.Generic;

namespace Pach_OS.Models
{
    public partial class Insumo
    {
        public Insumo()
        {
            DetallesCompras = new HashSet<DetallesCompra>();
            Proveedores = new HashSet<Proveedore>();
        }

        public int IdInsumos { get; set; }
        public string? NomInsumo { get; set; }
        public int? CantInsumo { get; internal set; }
        public int? ProveedoresId { get; set; }
        public string? TiempoLlegado { get; set; }

        public virtual ICollection<DetallesCompra> DetallesCompras { get; set; }
        public virtual ICollection<Proveedore> Proveedores { get; set; }
    }
}
