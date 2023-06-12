using System;
using System.Collections.Generic;

namespace Pach_OS.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleVenta = new HashSet<DetalleVenta>();
        }

        public int IdProductos { get; set; }
        public string? NomProducto { get; set; }
        public int? PrecioVenta { get; set; }
        public string? Estado { get; set; }
        public int? CategoriaId { get; set; }
        public int? CantInsumo { get; set; }
        public byte? TamanoPizza { get; set; }
        public byte? MaximoSabores { get; set; }

        public virtual Categoria? Categoria { get; set; }
        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
    }
}
