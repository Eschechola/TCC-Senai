using Lyfr_Admin.Models.Application.Interfaces;
using Lyfr_Admin.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Lyfr_Admin.Models.Application.Classes
{
    public class RepositoryCliente : IRepositoryCliente
    {
        private Uri uri;

        public RepositoryCliente()
        {
            uri = new Uri("http://lyfrapi.com.br/api/");
        }

        public Task<string> Add(string Token)
        {
            throw new NotImplementedException();
        }

        public Task<string> Alter(Cliente Entity, string Token)
        {
            throw new NotImplementedException();
        }

        public Task<string> Delete(int Id, string Token)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Cliente>> SelectAll(string Token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = uri;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    HttpResponseMessage response = await client.GetAsync("Cliente/GetAllClientes/");

                    string result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        List<Cliente> listCliente = JsonConvert.DeserializeObject<List<Cliente>>(result);

                        return listCliente;
                    }

                    if (result == null)
                    {
                        throw new Exception(response.StatusCode.ToString());
                    }

                    throw new Exception(result);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public Task<Cliente> SelectOne(Cliente Entity, string Token)
        {
            throw new NotImplementedException();
        }
    }
}
