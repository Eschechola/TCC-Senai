using Lyfr_Admin.Models.Entities;
using Lyfr_Admin.Requests.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Lyfr_Admin.Requests.PagesRequests
{
    public class ClienteRequest
    {
        public async Task<List<Cliente>> GetAllClientes(string token)
        {
            try
            {
                var jsonDeUsuarios = await new RequestAPI().GetApi("Cliente/GetAllClientes/0", token);
                var listaDeUsuario = JsonConvert.DeserializeObject<List<Cliente>>(jsonDeUsuarios);

                return listaDeUsuario;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Cliente> GetClienteByCpf(string token, string json)
        {
            try
            {
                var jsonDeUsuario = await new RequestAPI().PostApi("Cliente/GetClienteByCPF", token, json);
                var usuario = JsonConvert.DeserializeObject<Cliente>(jsonDeUsuario);
                return usuario;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
