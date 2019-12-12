using Lyfr.DAL.Interfaces;
using Lyfr.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lyfr.DAL.Repository
{
    public class RepositorySugestao : IRepositorySugestao
    {
        private Uri uri;

        public RepositorySugestao()
        {
            uri = new Uri("http://www.lyfrapi.com.br/api/");
        }

        public async Task Adicionar(Sugestao sugestao, string Token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = uri;
                    var json = JsonConvert.SerializeObject(sugestao);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    HttpResponseMessage response = await client.PostAsync("Sugestao/Insert/", content);
                    await response.Content.ReadAsStringAsync();
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode && mensagem == "Sugestao enviada com sucesso! Logo entraremos em contato.\nEquipe Lyfr agradece seu feedback")
                    {
                        return;
                    }

                    if (!string.IsNullOrWhiteSpace(mensagem))
                    {
                        throw new Exception(mensagem);
                    }

                    throw new Exception(response.StatusCode.ToString());

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public Task Alterar(Sugestao objeto, string Token)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(Sugestao objeto, string Token)
        {
            throw new NotImplementedException();
        }

        public Task<Sugestao> SelecionarPorId(int id, string Token)
        {
            throw new NotImplementedException();
        }

        public Task<List<Sugestao>> SelecionarTodos(string Token)
        {
            throw new NotImplementedException();
        }
    }
}
