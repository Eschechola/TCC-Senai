using Lyfr_Admin.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr_Admin.Models.Application.Interfaces
{
    public interface IRepositoryAdministrador : IRepositoryGeneric<Administrador>
    {
        Task AddIfNotExists(string Token);
    }
}
