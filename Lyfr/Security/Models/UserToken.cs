using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr.Security.Models
{
    public class UserToken
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string TipoUsuario { get; set; }
    }
}
