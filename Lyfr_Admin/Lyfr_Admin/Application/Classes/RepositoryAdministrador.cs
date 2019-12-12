using Lyfr_Admin.Models.Application.Interfaces;
using Lyfr_Admin.Models.Entities;
using Lyfr_Admin.Models.Models.Login;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lyfr_Admin.Models.Application.Classes
{
    public class RepositoryAdministrador : IRepositoryAdministrador
    {
        private Uri uri;

        public RepositoryAdministrador()
        {
            uri = new Uri("http://lyfrapi.com.br/api/");
        }

        public async Task<string> Add(string Token)
        {
            Administrador administrador = new Administrador()
            {
                Cpf = "00000000",
                Email = "exemplo@email.com",
                Login = "Admin",
                Senha = "Admin123"
            };

            using(HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = uri;
                    var json = JsonConvert.SerializeObject(administrador);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    HttpResponseMessage response = await client.PostAsync("Administrador/Insert/",content);

                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ToString();
                    }

                    throw new Exception(response.StatusCode.ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task AddIfNotExists(string Token)
        {
            try
            {
                var list_admin = await SelectAll(Token);

                if (list_admin == null)
                {
                    await Add(Token);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }     
        }

        public Task<string> Alter(Administrador Entity, string Token)
        {
            throw new NotImplementedException();
        }

        public Task<string> Delete(int Id, string Token)
        {
            throw new NotImplementedException();
        }

        public async Task<Administrador> SelectOne(Administrador administrador, string Token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = uri;
                    var json = JsonConvert.SerializeObject(administrador);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    HttpResponseMessage response = await client.PostAsync("Administrador/GetAdministrador/",content);

                    string result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        Administrador adm = JsonConvert.DeserializeObject<Administrador>(result);

                        return adm;                        
                    }

                    if(result == null)
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

        public async Task<string> ForgotPassword(RecoveryPassword recuperarSenha, string Token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var emailRecuperar = new RecoveryPassword
                    {
                        Email = recuperarSenha.Email,
                        CodigoGerado = recuperarSenha.CodigoGerado,
                    };

                    client.BaseAddress = uri;
                    var json = JsonConvert.SerializeObject(emailRecuperar);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    HttpResponseMessage response = await client.PostAsync("Administrador/ForgotPassword/", content);

                    string result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return result;
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

        public async Task<List<Administrador>> SelectAll(string Token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = uri;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    HttpResponseMessage response = await client.GetAsync("Administrador/GetAllAdministradores/");

                    string result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        List<Administrador> adm = JsonConvert.DeserializeObject<List<Administrador>>(result);

                        return adm;
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
    }
}
