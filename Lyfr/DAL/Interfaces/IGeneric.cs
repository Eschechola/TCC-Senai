using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr.DAL.Interfaces
{
    public interface IGeneric<T>
    {
        Task Adicionar(T objeto, string Token);
        Task Excluir(T objeto, string Token);
        Task<T> SelecionarPorId(int id, string Token);
        Task<List<T>> SelecionarTodos(string Token);
        Task Alterar(T objeto, string Token);
    }
}
