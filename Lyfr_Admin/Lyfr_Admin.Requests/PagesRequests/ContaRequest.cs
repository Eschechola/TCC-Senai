using Lyfr_Admin.Models.Entities;
using Lyfr_Admin.Requests.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lyfr_Admin.Requests.PagesRequests
{
    public class ContaRequest
    {
        public async Task<Administrador> GetAdministradorByLogin(string token, string json)
        {
            try
            {
                var jsonDeAdministrador = await new RequestAPI().PostApi("Administrador/GetAdministrador", token, json);
                var administrador = JsonConvert.DeserializeObject<Administrador>(jsonDeAdministrador);
                return administrador;
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

                throw;
            }
        }
    }
}
