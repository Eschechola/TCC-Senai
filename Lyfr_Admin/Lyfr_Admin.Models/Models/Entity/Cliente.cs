using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr_Admin.Models.Entities
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
