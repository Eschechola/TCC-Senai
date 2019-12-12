using Lyfr.Models;
using Lyfr.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr.DAL.Interfaces
{
    public interface IRepositoryHistorico : IGeneric<Historico>
    {
        Task<List<Historico>> SelectHistoricoByUsuario(int IdUsuario, string Token);
    }

}
