using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr_Admin.Models.Entities
{
    public class TokenJWT
    {
        public string TokenString { get; set; }
        public string HoraExpiracao { get; set; } 
    }
}
