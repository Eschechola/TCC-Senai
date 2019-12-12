using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr_Admin.Models.Entities
{
    public class Editora
    {
        public int IdEditora { get; set; }
        public string Nome { get; set; }

        public ICollection<Livros> Livros { get; set; }
    }
}
