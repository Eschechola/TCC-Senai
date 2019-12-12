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
    public class RepositoryFavoritos : IRepositoryFavoritos
    {
        private Uri uri;

        public RepositoryFavoritos()
        {
            uri = new Uri("http://www.lyfrapi.com.br/api/");
        }

        public async Task Adicionar(Favoritos favorito, string Token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = uri;
                    var json = JsonConvert.SerializeObject(favorito);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    HttpResponseMessage response = await client.PostAsync("Favoritos/Insert/", content);
                    await response.Content.ReadAsStringAsync();
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode && mensagem == "Favoritos cadastrado com sucesso!")
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

        public Task Alterar(Favoritos objeto, string Token)
        {
            throw new NotImplementedException();
        }

        public async Task Excluir(Favoritos objeto, string Token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = uri;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    HttpResponseMessage response = await client.DeleteAsync("Favoritos/DeleteById/"+objeto.FkIdCliente+"/"+objeto.FkIdLivro);
                    await response.Content.ReadAsStringAsync();
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode && mensagem == "O livro de id " + objeto.FkIdLivro + " foi deletado com sucesso da sua lista")
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

        public async Task<List<Livros>> GetLivrosByCliente(int idCliente, string Token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = uri;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    HttpResponseMessage response = await client.GetAsync("Favoritos/GetFavoritosByUsuario/"+idCliente);
                    await response.Content.ReadAsStringAsync();
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                         return JsonConvert.DeserializeObject<List<Livros>>(mensagem);
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

        public Task<Favoritos> SelecionarPorId(int id, string Token)
        {
            throw new NotImplementedException();
        }

        public Task<List<Favoritos>> SelecionarTodos(string Token)
        {
            throw new NotImplementedException();
        }
    }
}
