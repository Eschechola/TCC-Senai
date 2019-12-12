using Lyfr.Models;
using Lyfr.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr.DAL.Interfaces
{
    public interface IRepositoryCliente : IGeneric<Cliente>
    {
        Task<Cliente> SelectClienteByEmail(Cliente cliente, string Token);
        Task<Cliente> SelectClienteToUpdateByEmail(string email, string Token);
        Task RedefinirSenha(RecoveryPassword recovery, string Token);
        Task<Cliente> SelectClienteByCpf(Cliente cliente, string Token);
    }
}
