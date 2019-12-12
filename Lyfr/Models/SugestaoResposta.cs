using System;
using System.Collections.Generic;
using System.Text;

namespace Lyfr.Models
{
    public class SugestaoResposta
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public char Atendido { get; set; }
        public string Mensagem { get; set; }

    }
}
