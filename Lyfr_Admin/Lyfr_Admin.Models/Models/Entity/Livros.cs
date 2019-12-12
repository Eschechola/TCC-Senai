using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr_Admin.Models.Entities
{
    public class Livros
    {
        public int IdLivro { get; set; }

        [Required(ErrorMessage = "O Título deve ser inserido.")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }
        public int? FkAutor { get; set; }
        public int? FkEditora { get; set; }

        [Display(Name = "Ano de Lançamento")]
        [Required(ErrorMessage = "O ano de lançamento deve ser inserido")]
        [MinLength(4, ErrorMessage = "O ano está inválido")]
        public string Ano_Lanc { get; set; }

        [Display(Name = "Gênero")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "A sinopse deve ser inserida")]
        public string Sinopse { get; set; }

        public string Capa { get; set; }

        public string Arquivo { get; set; }

        [Required(ErrorMessage = "O ISBN deve ser inserido")]
        public string Isbn { get; set; }

        public string Idioma { get; set; }

        public float? IdMediaNota { get; set; }
        public int? TotalAcessos { get; set; }

        public Autores Autor { get; set; }
        public Editora Editora { get; set; }
        public ICollection<Favoritos> Favoritos { get; set; }
        public ICollection<Historico> Historico { get; set; }
        public ICollection<LivrosClientes> Livrosclientes { get; set; }
        public ICollection<PaginasMarcadas> Paginasmarcadas { get; set; }
    }
}
