using System;
using System.Collections.Generic;

namespace Pach_OS.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdCategoria { get; set; }
        public string? NomCategoria { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
