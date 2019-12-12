using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr_Admin.Models.Application.Interfaces
{
    public interface IRepositoryGeneric<T>
    {
        Task<string> Delete(int Id, string Token);
        Task<string> Alter(T Entity, string Token);
        Task<string> Add(string Token);
        Task<List<T>> SelectAll(string Token);
        Task<T> SelectOne(T Entity, string Token);
    }
}
