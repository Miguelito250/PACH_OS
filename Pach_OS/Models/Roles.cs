using System;
using System.Collections.Generic;

namespace Pach_OS.Models
{
    public partial class Roles
    {
        public Roles()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public byte IdRol { get; set; }
        public string? NomRol { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
