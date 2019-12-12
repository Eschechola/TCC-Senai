using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr_Admin.Models.Entities
{
    public class Favoritos
    {
        public int IdFavoritos { get; set; }
        public int? FkIdLivro { get; set; }
        public int? FkIdCliente { get; set; }

        public Cliente FkIdClienteNavigation { get; set; }
        public Livros FkIdLivroNavigation { get; set; }
    }
}
