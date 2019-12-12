using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Lyfr.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult Index(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                ViewBag.Erro = statusCode.ToString();

                if(statusCode.Value == 404)
                {
                    ViewBag.DetalhesErro = "Infelizmente, essa página não existe!";
                }

                if(statusCode.Value == 401)
                {
                    ViewBag.DetalhesErro = "Infelizmente, você não pode acessar essa página!";
                }

                if (statusCode.Value == 500)
                {
                    ViewBag.DetalhesErro = "Infelizmente, há algo de errado por aqui! Mas já estamos trabalhando em uma solução pra você.";
                }
            }
            return View();
        }
    }
}