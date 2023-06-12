using System;
using System.Collections.Generic;

namespace Pach_OS.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Compras = new HashSet<Compra>();
            Venta = new HashSet<Venta>();
        }

        public int IdUsuario { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NumDocumento { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Contrasena { get; set; }
        public string? Celular { get; set; }
        public string? Correo { get; set; }
        public byte? RolId { get; set; }
        public string? Estado { get; set; }
        public DateTime? FechaEntrada { get; set; }

        public virtual Roles? Rol { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
