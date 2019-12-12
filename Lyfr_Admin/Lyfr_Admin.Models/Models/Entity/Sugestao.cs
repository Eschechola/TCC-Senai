using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr_Admin.Models.Entities
{
    public class Sugestao
    {
        public int IdSugestao { get; set; }
        public int? FkIdCliente { get; set; }
        public string Mensagem { get; set; }
        public string Data_Cadastro { get; set; }

        public Cliente FkIdClienteNavigation { get; set; }
    }
}
