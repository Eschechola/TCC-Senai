using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lyfr.Models;
using Lyfr.DAL.Interfaces;
using Lyfr.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Lyfr.Models.Email;
using Microsoft.AspNetCore.Http;
using Lyfr.Email;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Lyfr.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private PhysicalFileProvider _provedorDiretoriosArquivos = new PhysicalFileProvider(Directory.GetCurrentDirectory());
        private readonly IRepositoryCliente _cliente;
        private readonly IRepositorySugestao _sugestao;
        private readonly IRepositoryGenero _genero;
        private readonly IRepositoryLivro _livro;
        private readonly IRepositoryGeral _geral;
        private readonly IRepositoryFavoritos _favoritos;
        private readonly IRepositoryHistorico _historico;

        public HomeController(
            IRepositoryCliente cliente, 
            IRepositorySugestao sugestao, 
            IRepositoryGenero genero, 
            IRepositoryLivro livro, 
            IRepositoryGeral geral, 
            IRepositoryFavoritos favoritos,
            IRepositoryHistorico historico)
        {
            _cliente = cliente;
            _sugestao = sugestao;
            _genero = genero;
            _livro = livro;
            _geral = geral;
            _favoritos = favoritos;
            _historico = historico;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            await Token.CheckCookies(HttpContext);

            try
            {
                ViewBag.Geral = await _geral.GetInfoGeral(Token.GetToken(HttpContext));
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
            }
            
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            try
            {
                await Token.CheckCookies(HttpContext);
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(Cliente cliente)
        {
            if (ModelState.GetFieldValidationState("Email") == ModelValidationState.Valid && ModelState.GetFieldValidationState("Senha") == ModelValidationState.Valid)
            {
                try
                {
                    await Token.CheckCookies(HttpContext);
                }
                catch (Exception)
                {

                }

                Cliente clienteLogado = new Cliente();

                try
                {
                    clienteLogado = await _cliente.SelectClienteByEmail(cliente, Token.GetToken(HttpContext));
                }
                catch (Exception ex)
                {
                    ViewBag.Erro = ex.Message;

                    return View();
                }

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, clienteLogado.Nome), //0
                    new Claim("CPF", clienteLogado.Cpf), //1
                    new Claim(ClaimTypes.Email, clienteLogado.Email),//2
                    new Claim("Id", clienteLogado.IdCliente.ToString()),//3
                    new Claim("Senha", clienteLogado.Senha),//4
                    new Claim(ClaimTypes.Role, "Cliente")
                };

                ClaimsIdentity usuarioIdentidade = new ClaimsIdentity(claims, "CookieAuthentication");

                ClaimsPrincipal principal = new ClaimsPrincipal(usuarioIdentidade);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(2),
                    IsPersistent = true
                });

                return RedirectToAction("Browse");
            }

            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Cadastro()
        {
            try
            {
                await Token.CheckCookies(HttpContext);
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Cadastro(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _cliente.Adicionar(cliente, Token.GetToken(HttpContext));
                }
                catch (Exception e)
                {
                    ViewBag.Erro = e.Message;
                }

                if(ViewBag.Erro == null)
                {
                    ViewBag.Sucesso = "Cadastrado com sucesso!";
                }
            }

            ViewBag.Cliente = cliente;
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Esqueci_Senha()
        {
            try
            {
                await Token.CheckCookies(HttpContext);
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Esqueci_Senha(Cliente Cliente)
        {
            if (ModelState.GetFieldValidationState("Senha") == ModelValidationState.Valid && ModelState.GetFieldValidationState("ConfSenha") == ModelValidationState.Valid)
            {
                try
                {
                    var clienteCompleto = await _cliente.SelectClienteToUpdateByEmail(Cliente.Email,Token.GetToken(HttpContext));
                    clienteCompleto.Senha = Cliente.Senha;
                    await _cliente.Alterar(clienteCompleto, Token.GetToken(HttpContext));
                }
                catch (Exception e)
                {
                    ViewBag.Erro = e.Message;
                    
                    return View();
                }
                ViewBag.Sucesso = "Senha alterada";
            }
            
            ViewBag.Cliente = Cliente;

            return View();
        }

        [AllowAnonymous]
        public async Task<string> RedefinirSenha(string Email)
        {
            RecoveryPassword recovery = new RecoveryPassword();
            recovery.Email = Email;
            recovery.CodigoGerado = EmailApplication.GenerateCode(HttpContext);

            try
            {
                await _cliente.RedefinirSenha(recovery, Token.GetToken(HttpContext));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Um código de redefinição foi enviado para o seu e-mail!";
        }

        [AllowAnonymous]
        public async Task<string> VerificarCodVerificacao(string CodEnviado)
        {
            await HttpContext.Session.LoadAsync();
            string cod = HttpContext.Session.GetString("CodigoEmail");

            if (CodEnviado == cod)
            {
                return "Código Correto";
            }
            else
            {
                return "Código inválido!";
            }
            
        }

        public async Task<IActionResult> Browse()
        {
            try
            {
                await Token.CheckCookies(HttpContext);
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
            }

            try
            {

                ViewBag.UltimosLivros = await _livro.GetLivros(0, Token.GetToken(HttpContext));
                ViewBag.Classico = await _livro.GetLivrosByGenero("Clássico", Token.GetToken(HttpContext));
                ViewBag.Drama = await _livro.GetLivrosByGenero("Drama", Token.GetToken(HttpContext));
                ViewBag.Contos = await _livro.GetLivrosByGenero("Contos", Token.GetToken(HttpContext));
                ViewBag.Aventura = await _livro.GetLivrosByGenero("Aventura", Token.GetToken(HttpContext));
                ViewBag.Favoritos = await _favoritos.GetLivrosByCliente(Int32.Parse(HttpContext.User.Claims.ToList()[3].Value), Token.GetToken(HttpContext));
            }
            catch (Exception ex)
            {
                ViewBag.ErroLivros = ex.Message;
            }
            
            return View();
        }

        public async Task<string> GetFavoritos()
        {
            try
            {
                var favoritos =  await _favoritos.GetLivrosByCliente(Int32.Parse(HttpContext.User.Claims.ToList()[3].Value), Token.GetToken(HttpContext));
                
                if(favoritos.Count <= 0)
                {
                    return "Vazio";
                }
                return JsonConvert.SerializeObject(favoritos);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        public async Task<string> GetLivroByTitle(string Titulo)
        {
            try
            {
                await Token.CheckCookies(HttpContext);
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
            }

            try
            {
                Livros livros = await _livro.GetLivrosByTitulo(Titulo, Token.GetToken(HttpContext));
                return JsonConvert.SerializeObject(livros);
            }
            catch (Exception ex)
            {
                return "Ocorreu algum erro!";
            }

        }

        [HttpPost]
        public async Task<bool> IsOnMyList(string NomeLivro)
        {
            try
            {
                var favoritos = await _favoritos.GetLivrosByCliente(Int32.Parse(HttpContext.User.Claims.ToList()[3].Value), Token.GetToken(HttpContext));
                foreach(var item in favoritos)
                {
                    if(item.Titulo == NomeLivro)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IActionResult> Generos()
        {
            try
            {
                await Token.CheckCookies(HttpContext);
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
            }

            List<Genero> listGeneros = new List<Genero>();

            try
            {
                listGeneros = await _genero.SelecionarTodos(Token.GetToken(HttpContext));
                listGeneros = listGeneros.OrderBy(x => x.Nome).ToList();
                if (listGeneros.Exists(x => x.Nome == "Destaques"))
                {
                    var dest = listGeneros.Find(x => x.Nome == "Destaques");
                    listGeneros.Remove(dest);
                    listGeneros.Insert(0, dest);
                }
                ViewBag.ListaGeneros = listGeneros;
            }
            catch (Exception ex)
            {
                ViewBag.ErroLivros = ex.Message;
            }
            
            return View();
        }

        [Route("/Home/Genero/{Nome}")]
        public async Task<IActionResult> Genero(string Nome)
        {
            try
            {
                await Token.CheckCookies(HttpContext);
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
            }

            try
            {
                ViewBag.Genero = await _genero.GetGeneroByNome(Nome, Token.GetToken(HttpContext));
                ViewBag.LivrosGenero = await _livro.GetLivrosByGenero(Nome, Token.GetToken(HttpContext));

            }
            catch (Exception ex)
            {
                ViewBag.ErroLivros = ex.Message;
            }
            
            return View();
        }

        [Route("/Home/Pesquisa/{Nome}")]
        public async Task<IActionResult> Pesquisa(string Nome)
        {
            try
            {
                ViewBag.Nome = Nome;
                await Token.CheckCookies(HttpContext);
                ViewBag.Pesquisa = await _livro.SearchLivros(Nome, Token.GetToken(HttpContext));
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
            }
            
            return View();
        }

        public async Task<IActionResult> MinhaLista()
        {
            try
            {
                await Token.CheckCookies(HttpContext);
                ViewBag.Favoritos = await _favoritos.GetLivrosByCliente(Int32.Parse(HttpContext.User.Claims.ToList()[3].Value), Token.GetToken(HttpContext));
            }
            catch (Exception)
            {

            }
            return View();
        }

        public async Task<IActionResult> Conta()
        {
            try
            {
                await Token.CheckCookies(HttpContext);
                ViewBag.Historico = await _historico.SelectHistoricoByUsuario(Convert.ToInt32(HttpContext.User.Claims.ToList()[3].Value), Token.GetToken(HttpContext));
            }
            catch (Exception)
            {

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Conta(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _cliente.Alterar(cliente, Token.GetToken(HttpContext));
                    ViewBag.Sucesso = "Alterado com sucesso!";
                }
                catch (Exception e)
                {
                    ViewBag.Erro = e.Message;
                }

            }
            return View();
        }

        [HttpPost]
        public async Task<string> EnviarSugestao(string mensagem)
        {
            Sugestao sugestao = new Sugestao();
            sugestao.Atendido = 'N';
            sugestao.FkIdCliente = Int32.Parse(HttpContext.User.Claims.ToList()[3].Value);
            sugestao.Mensagem = mensagem;

            try
            {
                await _sugestao.Adicionar(sugestao, Token.GetToken(HttpContext));
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return "Enviado! Obrigado pela sugestão ou por nos reportar um erro. Entraremos em contato em breve!";
        }

        [HttpPost]
        public string CheckSenha(string senha)
        {
            if(senha == HttpContext.User.Claims.ToList()[4].Value)
            {
                return "Certo";
            }
            else
            {
                return "Errado";
            }  
        }

        [HttpPost]
        public async Task<string> AlterarSenha(string senha)
        {
            Cliente cliente = new Cliente();
            cliente.Cpf = HttpContext.User.Claims.ToList()[1].Value;
            cliente.Email = HttpContext.User.Claims.ToList()[2].Value;
            cliente.IdCliente = Int32.Parse(HttpContext.User.Claims.ToList()[3].Value);
            cliente.Nome = HttpContext.User.Claims.ToList()[0].Value;
            cliente.Senha = senha;

            try
            {
                await _cliente.Alterar(cliente, Token.GetToken(HttpContext));
                return "Senha Alterada";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> AdicionarFavorito(int IdLivro)
        {
            Favoritos favorito = new Favoritos();
            favorito.FkIdLivro = IdLivro;
            favorito.FkIdCliente = Int32.Parse(HttpContext.User.Claims.ToList()[3].Value);

            try
            {
                await _favoritos.Adicionar(favorito, Token.GetToken(HttpContext));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "Adicionado";
           
        }

        public async Task<string> RemoverFavorito(int IdLivro)
        {
            Favoritos favorito = new Favoritos();
            favorito.FkIdLivro = IdLivro;
            favorito.FkIdCliente = Int32.Parse(HttpContext.User.Claims.ToList()[3].Value);

            try
            {
                await _favoritos.Excluir(favorito, Token.GetToken(HttpContext));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "Removido";

        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/Home/Reader/{Titulo}")]
        public async Task<IActionResult> Reader(string Titulo)
        {
            var livro = await _livro.GetLivrosByTitulo(Titulo, Token.GetToken(HttpContext));
            ViewBag.Livro = livro;

            Historico historico = new Historico();
            historico.FkIdCliente = Convert.ToInt32(HttpContext.User.Claims.ToList()[3].Value);
            historico.FkIdLivro = livro.IdLivro;
            historico.DataLeitura = DateTime.Now.ToString();

            await _historico.Adicionar(historico, Token.GetToken(HttpContext));
            return View();
        }
    }
}
