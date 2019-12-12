using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr.Models.Email
{
    public class RecoveryPassword
    {
        public string Email { get; set; }

        public string CodigoGerado { get; set; }
    }
}
