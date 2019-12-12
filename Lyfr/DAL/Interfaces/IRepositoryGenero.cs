using Lyfr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr.DAL.Interfaces
{
    public interface IRepositoryGenero : IGeneric<Genero>
    {
        Task<Genero> GetGeneroByNome(string nome, string Token);
    }
}
