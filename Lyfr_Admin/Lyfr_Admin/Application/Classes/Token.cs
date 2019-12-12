using Lyfr_Admin.Models.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lyfr_Admin.Models.Application.Classes
{
    public class Token
    {
        public static bool IsNeededANewToken(HttpContext context)
        {
            DateTime now = DateTime.Now;
            DateTime expiration = DateTime.ParseExact(context.Request.Cookies["TokenExpiration"], "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            if (now.CompareTo(expiration) >= 0)
            {
                return true;
            }

            return false;
        }

        public static async Task<TokenJWT> GenerateToken()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://lyfrapi.com.br/api/");

                try
                {
                    UserToken user = new UserToken();

                    user.Usuario = "Lyfr_User123";
                    user.Senha = "LyfrAPI123";
                    user.TipoUsuario = "W";

                    var json = JsonConvert.SerializeObject(user);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("Seguranca/Login", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var response_json = await response.Content.ReadAsStringAsync();

                        return JsonConvert.DeserializeObject<TokenJWT>(response_json);
                    }

                    throw new Exception(response.StatusCode.ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public static string GetToken(HttpContext context)
        {
            return context.Request.Cookies["Token"];
        }

        public async static Task CheckCookies(HttpContext context)
        {
            if (!String.IsNullOrEmpty(context.Request.Cookies["Token"]))
            {
                if (Token.IsNeededANewToken(context))
                {
                    await GenerateCookies(context);
                }
            }
            else
            {
                await GenerateCookies(context);
            }
        }

        private async static Task GenerateCookies(HttpContext context)
        {
            var token = await Token.GenerateToken();

            CookieOptions option = new CookieOptions();

            option.Expires = DateTime.Now.AddHours(2);

            context.Response.OnStarting(state => {
                var httpContext = (HttpContext)state;
                httpContext.Response.Cookies.Append("Token", token.TokenString, option);
                httpContext.Response.Cookies.Append("TokenExpiration", token.HoraExpiracao, option);
                return Task.FromResult(0);
            }, context);


            RepositoryAdministrador repository = new RepositoryAdministrador();
            await repository.AddIfNotExists(token.TokenString);
        }
    }
}
