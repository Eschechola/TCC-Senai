using Lyfr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr.DAL.Interfaces
{
    public interface IRepositoryLivro : IGeneric<Livros>
    {
        Task<List<Livros>> GetLivrosByGenero(string Genero, string Token);
        Task<Livros> GetLivrosByTitulo(string Titulo, string Token);
        Task<List<Livros>> GetLivros(int numeroDeLivros, string Token);
        Task<List<Livros>> SearchLivros(string Titulo, string Token);
    }
}
