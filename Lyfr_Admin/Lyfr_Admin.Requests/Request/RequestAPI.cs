using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lyfr_Admin.Requests.Request
{
    class RequestAPI
    {
        private Uri UrlRequisicao { get; set; }

        public RequestAPI()
        {
            UrlRequisicao = new Uri("http://www.lyfrapi.com.br/api/");
        }

        public async Task<string> PostApi(string url, string token, string json)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = UrlRequisicao;
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new Exception(response.StatusCode.ToString());
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }

        public async Task<string> DeleteApi(string url, string token, string json)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri("http://lyfrapi.com.br/api/" + url),
                        Content = new StringContent(json, Encoding.UTF8, "application/json"),
                    };

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new Exception(response.StatusCode.ToString());
                    }
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }

        }

        public async Task<string> PutApi(string url, string token, string json)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = UrlRequisicao;
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await client.PutAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new Exception(response.StatusCode.ToString());
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }

        public async Task<string> GetApi(string url, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = UrlRequisicao;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(url);

                    var content = await response.Content.ReadAsStringAsync();
                    return content;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
    }
}
