using Lyfr_Admin.Models.Entities;
using Lyfr_Admin.Requests.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lyfr_Admin.Requests.PagesRequests
{
    public class AdministradorRequest
    {
        public async Task<Administrador> GetAdminByEmail(string token, string json)
        {
            try
            {
                var jsonDeUsuario = await new RequestAPI().PostApi("Administrador/GetAdministradorByEmail", token, json);
                var usuario = JsonConvert.DeserializeObject<Administrador>(jsonDeUsuario);
                return usuario;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> Update(string token, string json)
        {
            try
            {
                var resposta = await new RequestAPI().PutApi("Administrador/Update", token, json);
                return resposta;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
