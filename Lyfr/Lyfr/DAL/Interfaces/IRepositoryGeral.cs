using Lyfr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr.DAL.Interfaces
{
    public interface IRepositoryGeral : IGeneric<GeralQuantidade>
    {
        Task<GeralQuantidade> GetInfoGeral(string Token);
    }
}
