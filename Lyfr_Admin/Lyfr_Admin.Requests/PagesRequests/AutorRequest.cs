using Lyfr_Admin.Models.Entities;
using Lyfr_Admin.Models.Models.Entity;
using Lyfr_Admin.Requests.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lyfr_Admin.Requests.PagesRequests
{
    public class AutorRequest
    {
        public async Task<string> Insert(Autores autor, string token)
        {
            try
            {
                var autorSerializado = JsonConvert.SerializeObject(autor);
                var resposta = await new RequestAPI().PostApi("Autor/Insert/", token, autorSerializado);
                
                return resposta;
            }
            catch (Exception)
            {
                return "Ocorreu algum erro ao se comunicar com a base de dados!";
            }
        }

        public async Task<List<Autores>> GetAllAutores(string token)
        {
            try
            {
                var jsonDeAutores = await new RequestAPI().GetApi("Autor/GetAllAutores/", token);
                var listaDeAutores = JsonConvert.DeserializeObject<List<Autores>>(jsonDeAutores);

                return listaDeAutores;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
