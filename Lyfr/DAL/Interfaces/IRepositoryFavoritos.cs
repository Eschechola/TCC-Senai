using Lyfr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr.DAL.Interfaces
{
    public interface IRepositoryFavoritos : IGeneric<Favoritos>
    {
        Task<List<Livros>> GetLivrosByCliente(int idCliente, string Token);
    }
}
