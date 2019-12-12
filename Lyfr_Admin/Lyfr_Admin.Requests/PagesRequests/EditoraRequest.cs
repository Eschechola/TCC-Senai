using Lyfr_Admin.Models.Entities;
using Lyfr_Admin.Requests.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lyfr_Admin.Requests.PagesRequests
{
    public class EditoraRequest
    {
        public async Task<List<Editora>> GetAllEditoras(string token)
        {
            try
            {
                var jsonDeEditoras = await new RequestAPI().GetApi("Editora/GetAllEditoras/", token);
                var listaDeEditoras = JsonConvert.DeserializeObject<List<Editora>>(jsonDeEditoras);

                return listaDeEditoras;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> Insert(Editora editora, string token)
        {
            try
            {
                var editoraSerializado = JsonConvert.SerializeObject(editora);
                var resposta = await new RequestAPI().PostApi("Editora/Insert/", token, editoraSerializado);

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
                var resposta = await new RequestAPI().DeleteApi("Editora/DeleteByNome/", token, nomeSerializado);

                return resposta;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
