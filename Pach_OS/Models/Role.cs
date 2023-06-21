using System;
using System.Collections.Generic;

namespace Pach_OS.Models
{
    public partial class Role
    {
        public Role()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public byte IdRol { get; set; }
        public string? NomRol { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
