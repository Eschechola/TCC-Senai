using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr_Admin.Models.Entities
{
    public class Administrador
    {
        [Required(ErrorMessage = "Campo necessário")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo necessário")]
        public string Senha { get; set; }

        [EmailAddress(ErrorMessage = "Digite um e-mail válido")]
        [Required(ErrorMessage = "Campo é necessário")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo é necessário")]
        public string Cpf { get; set; }
    }
}
