using Lyfr_Admin.Models.Models.Entity;
using Lyfr_Admin.Requests.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lyfr_Admin.Requests.PagesRequests
{
    public class SugestaoRequest
    {
        public async Task<List<SugestaoResposta>> GetAllSugestoes(string token)
        {
            try
            {
                var jsonDeUsuarios = await new RequestAPI().GetApi("Sugestao/GetAllSugestoes/", token);
                var listaDeUsuario = JsonConvert.DeserializeObject<List<SugestaoResposta>>(jsonDeUsuarios);

                return listaDeUsuario;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> EnviarRespostaSugestao(SugestaoResposta respostaSugestao, string token)
        {
            try
            {
                var json = JsonConvert.SerializeObject(respostaSugestao);
                var resposta = await new RequestAPI().PostApi("Sugestao/SugestaoResposta/", token, json);

                return resposta;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
