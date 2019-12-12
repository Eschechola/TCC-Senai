using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr_Admin.Models.Entities
{
    public class Autores
    {
        public int IdAutor { get; set; }
        public string Nome { get; set; }
        public string AnoNasc { get; set; }
        public string Bio { get; set; }
        public string Foto { get; set; }

        public ICollection<Livros> Livros { get; set; }
    }
}
