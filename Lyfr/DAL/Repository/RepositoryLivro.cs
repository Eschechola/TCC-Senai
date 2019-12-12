using Lyfr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lyfr.DAL.Interfaces;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Lyfr.DAL.Repository
{
    public class RepositoryLivro : IRepositoryLivro
    {

        private Uri uri;

        private PhysicalFileProvider _provedorDiretoriosArquivos = new PhysicalFileProvider(Directory.GetCurrentDirectory());

        private string diretorioLivros = "wwwroot/Livros/Epubs";
        
        public RepositoryLivro()
        {
            uri = new Uri("http://www.lyfrapi.com.br/api/");
        }

        public Task Adicionar(Livros objeto, string Token)
        {
            throw new NotImplementedException();
        }

        public Task Alterar(Livros objeto, string Token)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(Livros objeto, string Token)
        {
            throw new NotImplementedException();
        }

        public Task<Livros> SelecionarPorId(int id, string Token)
        {
            throw new NotImplementedException();
        }

        public Task<List<Livros>> SelecionarTodos(string Token)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Livros>> GetLivrosByGenero(string Genero, string Token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = uri;
                    var content = new StringContent("\"" + Genero + "\"", Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    HttpResponseMessage response = await client.PostAsync("Livros/GetLivrosByGenero/", content);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode == true)
                    {
                        List<Livros> livros = JsonConvert.DeserializeObject<List<Livros>>(mensagem);
                        return livros;
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

        public async Task<List<Livros>> GetLivros(int numeroDeLivros, string Token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = uri;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    HttpResponseMessage response = await client.GetAsync("Livros/GetAllLivros/"+numeroDeLivros);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode == true)
                    {
                        List<Livros> livros = JsonConvert.DeserializeObject<List<Livros>>(mensagem);
                        return livros;
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

        public async Task<Livros> GetLivrosByTitulo(string Titulo, string Token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = uri;
                    var content = new StringContent("\"" + Titulo + "\"", Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                     HttpResponseMessage response = await client.PostAsync("Livros/GetLivroByTituloWithoutFile/", content);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode == true)
                    {
                        Livros livros = JsonConvert.DeserializeObject<Livros>(mensagem);

                        return livros;
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

        public async Task<List<Livros>> SearchLivros(string Titulo, string Token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = uri;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    HttpResponseMessage response = await client.GetAsync("Livros/Search/"+Titulo);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode == true)
                    {
                        List<Livros> livros = JsonConvert.DeserializeObject<List<Livros>>(mensagem);
                        return livros;
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
