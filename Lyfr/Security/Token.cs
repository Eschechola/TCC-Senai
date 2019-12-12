using Lyfr.Security.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lyfr.Security
{
    public class Token
    {
        public static string GetToken(HttpContext context)
        {
            var token = context.Request.Cookies["Token"];

            return token;
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

        private static bool IsNeededANewToken(HttpContext context)
        {
            DateTime now = DateTime.Now;
            DateTime expiration = DateTime.ParseExact(context.Request.Cookies["TokenExpiration"], "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            if (now.CompareTo(expiration) >= 0)
            {
                return true;
            }

            return false;
        }

        private async static Task GenerateCookies(HttpContext context)
        {
            var token = await Token.GenerateToken();

            CookieOptions option = new CookieOptions();

            option.Expires = DateTime.Now.AddHours(2);

            context.Response.Cookies.Append("Token", token.TokenString, option);
            context.Response.Cookies.Append("TokenExpiration", token.HoraExpiracao, option);
        }

        private static async Task<TokenJWT> GenerateToken()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.lyfrapi.com.br/api/");

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
    }
}
