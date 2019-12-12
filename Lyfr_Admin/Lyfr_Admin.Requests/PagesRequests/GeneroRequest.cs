using Lyfr_Admin.Models.Entities;
using Lyfr_Admin.Requests.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lyfr_Admin.Requests.PagesRequests
{
    public class GeneroRequest
    {
        public async Task<List<Genero>> GetAllGeneros(string token)
        {
            try
            {
                var jsonDeGeneros = await new RequestAPI().GetApi("Genero/GetAllGeneros/", token);
                var listaDeGeneros = JsonConvert.DeserializeObject<List<Genero>>(jsonDeGeneros);

                return listaDeGeneros;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<string> Insert(Genero genero, string token)
        {
            try
            {
                var generoSerializado = JsonConvert.SerializeObject(genero);
                var resposta = await new RequestAPI().PostApi("Genero/Insert/", token, generoSerializado);

                return resposta;
            }
            catch (Exception)
            {
                return "Ocorreu algum erro ao se comunicar com a base de dados!";
            }
        }

        public async Task<string> Delete(string nome, string token)
        {
            try
            {
                var nomeSerializado = JsonConvert.SerializeObject(nome);
                var resposta = await new RequestAPI().DeleteApi("Genero/DeleteByNome/", token, nomeSerializado);

                return resposta;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
