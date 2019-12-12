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
    public class RepositoryGeral : IRepositoryGeral
    {
        private Uri uri;

        public RepositoryGeral()
        {
            uri = new Uri("http://www.lyfrapi.com.br/api/");
        }

        public Task Adicionar(GeralQuantidade objeto, string Token)
        {
            throw new NotImplementedException();
        }

        public Task Alterar(GeralQuantidade objeto, string Token)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(GeralQuantidade objeto, string Token)
        {
            throw new NotImplementedException();
        }

        public Task<GeralQuantidade> SelecionarPorId(int id, string Token)
        {
            throw new NotImplementedException();
        }

        public Task<List<GeralQuantidade>> SelecionarTodos(string Token)
        {
            throw new NotImplementedException();
        }

        public async Task<GeralQuantidade> GetInfoGeral(string Token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = uri;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    HttpResponseMessage response = await client.GetAsync("Geral/GetInformacoesSite");
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode == true)
                    {
                        GeralQuantidade geral = JsonConvert.DeserializeObject<GeralQuantidade>(mensagem);
                        return geral;
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

    }
}
