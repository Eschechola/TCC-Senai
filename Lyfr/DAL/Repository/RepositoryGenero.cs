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
    public class RepositoryGenero : IRepositoryGenero
    {
        private Uri uri;

        public RepositoryGenero()
        {
            uri = new Uri("http://www.lyfrapi.com.br/api/");
        }

        public Task Adicionar(Genero objeto, string Token)
        {
            throw new NotImplementedException();
        }

        public Task Alterar(Genero objeto, string Token)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(Genero objeto, string Token)
        {
            throw new NotImplementedException();
        }

        public async Task<Genero> GetGeneroByNome(string nome, string Token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = uri;
                    var content = new StringContent("\"" + nome + "\"", Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    HttpResponseMessage response = await client.PostAsync("Genero/GetGeneroByNome/", content);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode == true)
                    {
                        Genero genero = JsonConvert.DeserializeObject<Genero>(mensagem);
                        return genero;
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

        public Task<Genero> SelecionarPorId(int id, string Token)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Genero>> SelecionarTodos(string Token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = uri;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    HttpResponseMessage response = await client.GetAsync("Genero/GetAllGeneros/");
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode == true)
                    {
                        List<Genero> list = JsonConvert.DeserializeObject<List<Genero>>(mensagem);
                        return list;
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
