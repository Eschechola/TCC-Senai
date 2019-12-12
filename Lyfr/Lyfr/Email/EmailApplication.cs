using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lyfr.Email
{
    public static class EmailApplication
    {
        public static string GenerateCode(HttpContext context)
        {
            Random random = new Random();
            string code = random.Next(0, 10000).ToString();
            SaveCode(context, code);
            return code;
        }

        public static void SaveCode(HttpContext context, string code)
        {
            context.Session.SetString("CodigoEmail", code);
        }
    }
}
