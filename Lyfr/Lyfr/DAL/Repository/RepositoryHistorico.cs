using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lyfr.Models;
using Lyfr.DAL.Interfaces;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using Lyfr.Models.Email;

namespace Lyfr.DAL.Repository
{
    public class RepositoryHistorico : IRepositoryHistorico
    {
        private Uri uri;

        public RepositoryHistorico()
        {
            uri = new Uri("http://www.lyfrapi.com.br/api/");
        }

        public async Task<List<Historico>> SelectHistoricoByUsuario(int IdUsuario, string Token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = uri;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    HttpResponseMessage response = await client.GetAsync("Historico/GetHistoricoByUsuario/"+IdUsuario);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode == true)
                    {
                        List<Historico> historico = JsonConvert.DeserializeObject<List<Historico>>(mensagem);
                        return historico;
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

        public async Task Adicionar(Historico objeto, string Token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = uri;
                    var json = JsonConvert.SerializeObject(objeto);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    HttpResponseMessage response = await client.PostAsync("Historico/Insert/", content);
                    await response.Content.ReadAsStringAsync();
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode && mensagem == "Histórico cadastrado com sucesso!")
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

        public Task Alterar(Historico objeto, string Token)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(Historico objeto, string Token)
        {
            throw new NotImplementedException();
        }

        public Task<Historico> SelecionarPorId(int id, string Token)
        {
            throw new NotImplementedException();
        }

        public Task<List<Historico>> SelecionarTodos(string Token)
        {
            throw new NotImplementedException();
        }

        
    }
}
