using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pach_OS.Models
{
    public partial class ProductosInsumo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? ProductosId { get; set; }
        public int? CantInsumo { get; set; }
        public int? InsumosId { get; set; }

        public virtual Insumo? Insumos { get; set; }
        public virtual Producto? Productos { get; set; }
    }
}
