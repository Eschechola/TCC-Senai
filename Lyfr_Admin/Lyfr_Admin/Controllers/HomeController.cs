using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lyfr_Admin.Models.Entities;
using Lyfr_Admin.Models.Application.Classes;
using Microsoft.AspNetCore.Mvc;
using Lyfr_Admin.Models.Application.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using Lyfr_Admin.Requests;
using System.Net.Http;
using System.Net.Http.Headers;
using Lyfr_Admin.Requests.PagesRequests;
using Lyfr_Admin.Models.Models.Login;
using Lyfr_Admin.Models.Models.Entity;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Lyfr_Admin.Files;

namespace Lyfr_Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly IRepositoryAdministrador _repositoryAdministrador;
        private readonly IRepositoryCliente _repositoryCliente;

        public HomeController(IRepositoryAdministrador repositoryAdministrador, IRepositoryCliente repositoryCliente, IHostingEnvironment IHostingEnvironment)
        {
            _repositoryAdministrador = repositoryAdministrador;
            _repositoryCliente = repositoryCliente;
            _environment = IHostingEnvironment;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            if (TempData["email"] != null)
            {
                ViewBag.Erro = TempData["email"];
            }

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Painel");
            }

            await Token.CheckCookies(HttpContext);

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(Administrador adm)
        {
            if (ModelState.GetFieldValidationState("Login") == ModelValidationState.Valid && ModelState.GetFieldValidationState("Senha") == ModelValidationState.Valid)
            {

                await Token.CheckCookies(HttpContext);

                Administrador administrador = new Administrador();

                try
                {
                    administrador = await _repositoryAdministrador.SelectOne(adm, Token.GetToken(HttpContext));
                }
                catch (Exception ex)
                {
                    ViewBag.Erro = ex.Message;

                    return View();
                }

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, administrador.Login),
                    new Claim(ClaimTypes.Email, administrador.Email),
                    new Claim("Senha", administrador.Senha),
                    new Claim(ClaimTypes.Role, "Administrador")
                };

                ClaimsIdentity usuarioIdentidade = new ClaimsIdentity(claims, "CookieAuthentication");

                ClaimsPrincipal principal = new ClaimsPrincipal(usuarioIdentidade);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(2),
                    IsPersistent = true
                });



                return RedirectToAction("Painel");
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> EsqueciSenha(RecoveryPassword recuperarSenha)
        {
            await Token.CheckCookies(HttpContext);

            try
            {
                var resposta = await new RepositoryAdministrador().ForgotPassword(recuperarSenha, Token.GetToken(HttpContext));

                if (resposta.Equals("Email enviado com sucesso!"))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Admnistrador não encontrado...");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
            }
        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> AlterarSenha(string senha, string confirmacaoSenha, string email)
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);

            try
            {
                if (senha.Equals(confirmacaoSenha))
                {
                    var json = JsonConvert.SerializeObject(email);
                    var pegarAdmin = await new AdministradorRequest().GetAdminByEmail(token, json);

                    if (pegarAdmin != null)
                    {
                        var adminAlterar = pegarAdmin;
                        adminAlterar.Senha = senha;

                        var jsonAtualizado = JsonConvert.SerializeObject(adminAlterar);
                        var resposta = await new AdministradorRequest().Update(token, jsonAtualizado);

                        return Ok(resposta);
                    }
                    else
                    {
                        return Ok("Ocorreu algum erro, tente novamente");
                    }
                }
                else
                {
                    return Ok("As senhas não são iguais...");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
            }
        }

        public IActionResult Painel()
        {
            //Logout();
            return View();
        }

        public async Task<IActionResult> Clientes(int idOrdenacao = 0)
        {
            /*
             * id = 0 - Padrão
             * id = 1 - Crescente
             * id = 2 - Decrescente
             * id = 3 - Nome
             * id = 4 - Email
             */

            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);

            ViewBag.Titulo = "Clientes";

            try
            {
                var listaDeClientes = await new ClienteRequest().GetAllClientes(token);

                switch (idOrdenacao)
                {
                    case 1:
                        listaDeClientes = listaDeClientes.OrderBy(x => x.IdCliente).ToList();
                        ViewBag.Ordenacao = "Crescente";
                        break;

                    case 2:
                        listaDeClientes = listaDeClientes.OrderByDescending(x => x.IdCliente).ToList();
                        ViewBag.Ordenacao = "Decrescente";
                        break;

                    case 3:
                        listaDeClientes = listaDeClientes.OrderBy(x => x.Nome).ToList();
                        ViewBag.Ordenacao = "Nome";
                        break;

                    case 4:
                        listaDeClientes = listaDeClientes.OrderBy(x => x.Email).ToList();
                        ViewBag.Ordenacao = "Email";
                        break;

                    default:
                        listaDeClientes = listaDeClientes.OrderByDescending(x => x.IdCliente).ToList();
                        ViewBag.Ordenacao = "Decrescente";
                        break;
                }

                return View(listaDeClientes);
            }
            catch (Exception)
            {
                return View(null);
            }
        }

        public async Task<IActionResult> DetClientes(string cpf, string senha)
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);
            //serializa os dados passados
            var usuarioSerializado = JsonConvert.SerializeObject(new ClienteLogin { Cpf = cpf, Senha = senha });
            //pega o usuario através dos dados
            var usuario = await new ClienteRequest().GetClienteByCpf(token, usuarioSerializado);

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Clientes(string cpf = "")
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);

            ViewBag.Titulo = "Clientes com o cpf: " + cpf;
            try
            {
                var listaDeClientes = await new ClienteRequest().GetAllClientes(token);
                var clientesRetornar = listaDeClientes.Where(x => x.Cpf.Contains(cpf)).ToList();
                return View(clientesRetornar);
            }
            catch (Exception)
            {
                return View(null);
            }
        }


        public async Task<IActionResult> Livros(int idOrdenacao = 0)
        {
            await Token.CheckCookies(HttpContext);

            var token = Token.GetToken(HttpContext);
            ViewBag.Titulo = "Livros ";

            try
            {
                var listaDeLivros = await new LivroRequest().GetAllLivros(token);

                /*
                 1 - Ordem cresde
                 2 - Ordem decrescente
                 3 - Ordem crescente por nome
                */

                switch (idOrdenacao)
                {
                    case 0:
                        listaDeLivros = listaDeLivros.OrderBy(x => x.IdLivro).ToList();
                        ViewBag.Ordenacao = "Crescente";
                        break;

                    case 1:
                        listaDeLivros = listaDeLivros.OrderByDescending(x => x.IdLivro).ToList();
                        ViewBag.Ordenacao = "Decrescente";
                        break;

                    case 2:
                        listaDeLivros = listaDeLivros.OrderBy(x => x.Titulo).ToList();
                        ViewBag.Ordenacao = "Nome";
                        break;

                    default:
                        listaDeLivros = listaDeLivros.OrderBy(x => x.IdLivro).ToList();
                        ViewBag.Ordenacao = "Crescente";
                        break;
                }


                return View(listaDeLivros);
            }
            catch (Exception)
            {
                return View(null);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Livros(string titulo = "")
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);

            ViewBag.Titulo = "Livros com o título: " + titulo;
            try
            {
                var listaDeLivros = await new LivroRequest().GetAllLivros(token);
                var livrosRetornar = listaDeLivros.Where(x => x.Titulo.ToLower().Contains(titulo.ToLower())).ToList();
                return View(livrosRetornar);
            }
            catch (Exception)
            {
                return View(null);
            }
        }

        public async Task<IActionResult> AdicionarLivros()
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);
            try
            {
                var Autores = await new AutorRequest().GetAllAutores(token);
                var Editoras = await new EditoraRequest().GetAllEditoras(token);
                var Generos = await new GeneroRequest().GetAllGeneros(token);
                ViewBag.Autores = Autores;
                ViewBag.Editoras = Editoras;
                ViewBag.Generos = Generos;
            }
            catch (Exception)
            {

                return View(null);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarLivros(Livros livros, IFormFile file, IFormFile capa)
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);

            try
            {
                if (ModelState.IsValid)
                {

                    if (file != null)
                    {
                        var filePath = Path.GetTempFileName();

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await file.CopyToAsync(stream);
                        }
                        livros.Arquivo = new FilesManipulation().ConverterArquivoBase64(filePath);
                    }

                    if (capa != null)
                    {
                        var filePath = Path.GetTempFileName();

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await capa.CopyToAsync(stream);
                        }
                        livros.Capa = new FilesManipulation().ConverterArquivoBase64(filePath);
                    }

                    if (capa != null && file != null)
                    {
                        var resposta = await new LivroRequest().Insert(livros, token);
                        ViewBag.Info = resposta;
                    }
                    else
                    {
                        ViewBag.Info = "Capa e livro não podem ficar em branco!";
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.Info = ex.ToString();
            }
            var Autores = await new AutorRequest().GetAllAutores(token);
            var Editoras = await new EditoraRequest().GetAllEditoras(token);
            var Generos = await new GeneroRequest().GetAllGeneros(token);
            ViewBag.Autores = Autores;
            ViewBag.Editoras = Editoras;
            ViewBag.Generos = Generos;
            return View();
        }

        [Route("{controller}/DetLivros/{titulo}")]
        public async Task<IActionResult> DetLivros(string titulo)
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);
            try
            {
                var Livro = await new LivroRequest().GetLivroByTitulo(titulo, token);
                return View(Livro);
            }
            catch (Exception)
            {
                return View(null);
            }

        }

        [Route("{controller}/AlterarLivros/{Titulo}")]
        public async Task<IActionResult> AlterarLivros(string Titulo)
        {
            Livros Livro = new Livros();
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);
            try
            {
                Livro = await new LivroRequest().GetLivroByTitulo(Titulo, token);
                var Autores = await new AutorRequest().GetAllAutores(token);
                var Editoras = await new EditoraRequest().GetAllEditoras(token);
                var Generos = await new GeneroRequest().GetAllGeneros(token);
                ViewBag.Autores = Autores;
                ViewBag.Editoras = Editoras;
                ViewBag.Generos = Generos;
            }
            catch (Exception)
            {
                return View(null);
            }

            return View(Livro);
        }

        [HttpPost]
        public async Task<IActionResult> AlterarLivros(Livros livros, IFormFile file, IFormFile capa)
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);

            try
            {
                if (ModelState.IsValid)
                {

                    if (file != null)
                    {
                        var filePath = Path.GetTempFileName();

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await file.CopyToAsync(stream);
                        }
                        livros.Arquivo = new FilesManipulation().ConverterArquivoBase64(filePath);
                    }

                    if (capa != null)
                    {
                        var filePath = Path.GetTempFileName();

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await capa.CopyToAsync(stream);
                        }
                        livros.Capa = new FilesManipulation().ConverterArquivoBase64(filePath);
                    }

                    var resposta = await new LivroRequest().Update(livros, token);
                    ViewBag.Info = resposta;

                }

            }
            catch (Exception ex)
            {
                ViewBag.Info = ex.ToString();
            }
            var Autores = await new AutorRequest().GetAllAutores(token);
            var Editoras = await new EditoraRequest().GetAllEditoras(token);
            var Generos = await new GeneroRequest().GetAllGeneros(token);
            ViewBag.Autores = Autores;
            ViewBag.Editoras = Editoras;
            ViewBag.Generos = Generos;
            return View();
        }

        [HttpDelete]
        public async Task<string> DeletarLivro(string nomeLivro)
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);

            try
            {
                var deletarLivroMensagem = await new LivroRequest().Delete(nomeLivro, token);
                return deletarLivroMensagem;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public async Task<IActionResult> Generos(int idOrdenacao = 0)
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);

            try
            {
                var listaDeGeneros = await new GeneroRequest().GetAllGeneros(token);

                /*
                 1 - Ordem cresde
                 2 - Ordem decrescente
                 3 - Ordem crescente por nome
                */

                switch (idOrdenacao)
                {
                    case 0:
                        listaDeGeneros = listaDeGeneros.OrderBy(x => x.IdGenero).ToList();
                        ViewBag.Ordenacao = "Crescente";
                        break;

                    case 1:
                        listaDeGeneros = listaDeGeneros.OrderByDescending(x => x.IdGenero).ToList();
                        ViewBag.Ordenacao = "Decrescente";
                        break;

                    case 2:
                        listaDeGeneros = listaDeGeneros.OrderBy(x => x.Nome).ToList();
                        ViewBag.Ordenacao = "Nome";
                        break;

                    default:
                        listaDeGeneros = listaDeGeneros.OrderBy(x => x.IdGenero).ToList();
                        ViewBag.Ordenacao = "Crescente";
                        break;
                }

                return View(listaDeGeneros);
            }
            catch (Exception)
            {
                return View(null);
            }
        }

        public async Task<IActionResult> AdicionarGenero()
        {
            await Token.CheckCookies(HttpContext);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarGenero(Genero genero)
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);

            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            if (HttpContext.Request.Form.Files != null)
                            {
                                //pega todos os arquivos enviados via POST
                                var arquivos = HttpContext.Request.Form.Files;

                                //chama a função que vai salvar a imagem na pasta indicada
                                var diretorioImagem = new FilesManipulation().SalvarFoto(_environment.WebRootPath, "imgGeneros", arquivos);
                                genero.Foto = new FilesManipulation().ConverterArquivoBase64(diretorioImagem);

                                var resposta = await new GeneroRequest().Insert(genero, token);

                                ViewBag.Info = resposta;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Info = ex.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Info = ex.ToString();
            }
            return View();
        }

        public async Task<IActionResult> Autores(int idOrdenacao = 0)
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);


            try
            {
                var listaDeAutores = await new AutorRequest().GetAllAutores(token);

                /*
                 1 - Ordem cresde
                 2 - Ordem decrescente
                 3 - Ordem crescente por nome
                */

                switch (idOrdenacao)
                {
                    case 0:
                        listaDeAutores = listaDeAutores.OrderBy(x => x.IdAutor).ToList();
                        ViewBag.Ordenacao = "Crescente";
                        break;

                    case 1:
                        listaDeAutores = listaDeAutores.OrderByDescending(x => x.IdAutor).ToList();
                        ViewBag.Ordenacao = "Decrescente";
                        break;

                    case 2:
                        listaDeAutores = listaDeAutores.OrderBy(x => x.Nome).ToList();
                        ViewBag.Ordenacao = "Nome";
                        break;

                    default:
                        listaDeAutores = listaDeAutores.OrderBy(x => x.IdAutor).ToList();
                        ViewBag.Ordenacao = "Crescente";
                        break;
                }


                return View(listaDeAutores);
            }
            catch (Exception)
            {
                return View(null);
            }
        }

        public async Task<IActionResult> DetAutor(string nome)
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);

            try
            {
                var listaDeAutores = await new AutorRequest().GetAllAutores(token);
                var autor = listaDeAutores.Where(x => x.Nome.Equals(nome)).FirstOrDefault();

                return View(autor);
            }
            catch (Exception)
            {
                return View(null);
            }
        }

        public async Task<IActionResult> AdicionarAutor()
        {
            await Token.CheckCookies(HttpContext);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarAutor(Autores autor)
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);

            try
            {
                if (ModelState.IsValid)
                {
                    if (HttpContext.Request.Form.Files != null)
                    {
                        //pega todos os arquivos enviados via POST
                        var arquivos = HttpContext.Request.Form.Files;

                        //chama a função que vai salvar a imagem na pasta indicada
                        var diretorioImagem = new FilesManipulation().SalvarFoto(_environment.WebRootPath, "imgsAutores\\Imagens", arquivos);
                        autor.Foto = new FilesManipulation().ConverterArquivoBase64(diretorioImagem);

                        var resposta = await new AutorRequest().Insert(autor, token);

                        ViewBag.Info = resposta;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Info = ex.ToString();
            }

            return View();
        }


        public async Task<IActionResult> AlterarAutor()
        {
            await Token.CheckCookies(HttpContext);

            return View();
        }

        public async Task<IActionResult> Editoras(int idOrdenacao)
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);

            try
            {
                var listaDeEditoras = await new EditoraRequest().GetAllEditoras(token);

                /*
                 1 - Ordem cresde
                 2 - Ordem decrescente
                 3 - Ordem crescente por nome
                */

                switch (idOrdenacao)
                {
                    case 0:
                        listaDeEditoras = listaDeEditoras.OrderBy(x => x.IdEditora).ToList();
                        ViewBag.Ordenacao = "Crescente";
                        break;

                    case 1:
                        listaDeEditoras = listaDeEditoras.OrderByDescending(x => x.IdEditora).ToList();
                        ViewBag.Ordenacao = "Decrescente";
                        break;

                    case 2:
                        listaDeEditoras = listaDeEditoras.OrderBy(x => x.Nome).ToList();
                        ViewBag.Ordenacao = "Nome";
                        break;

                    default:
                        listaDeEditoras = listaDeEditoras.OrderBy(x => x.IdEditora).ToList();
                        ViewBag.Ordenacao = "Crescente";
                        break;
                }

                return View(listaDeEditoras);
            }
            catch (Exception)
            {
                return View(null);
            }
        }

        public async Task<IActionResult> DetEditora()
        {
            await Token.CheckCookies(HttpContext);

            return View();
        }

        public async Task<IActionResult> AdicionarEditora()
        {
            await Token.CheckCookies(HttpContext);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarEditora(Editora editora)
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);

            try
            {
                if (ModelState.IsValid)
                {
                    var resposta = await new EditoraRequest().Insert(editora, token);
                    ViewBag.Info = resposta;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Info = ex.ToString();
            }
            return View();
        }

        public async Task<IActionResult> AlterarEditora()
        {
            await Token.CheckCookies(HttpContext);

            return View();
        }

        public async Task<IActionResult> Sugestoes(int idOrdenacao = 0)
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);

            try
            {
                var listaDeSugestoes = await new SugestaoRequest().GetAllSugestoes(token);

                switch (idOrdenacao)
                {
                    case 0:
                        listaDeSugestoes = listaDeSugestoes.OrderBy(x => x.Id).ToList();
                        ViewBag.Ordenacao = "ID";
                        break;

                    case 1:
                        listaDeSugestoes = listaDeSugestoes.OrderBy(x => x.Id).ToList();
                        ViewBag.Ordenacao = "ID";
                        break;

                    case 2:
                        listaDeSugestoes = listaDeSugestoes.OrderByDescending(x => x.Atendido).ToList();
                        ViewBag.Ordenacao = "Atendidos";
                        break;

                    case 3:
                        listaDeSugestoes = listaDeSugestoes.OrderBy(x => x.Atendido).ToList();
                        ViewBag.Ordenacao = "Não Atendidos";
                        break;

                    default:
                        listaDeSugestoes = listaDeSugestoes.OrderBy(x => x.Atendido).ToList();
                        ViewBag.Ordenacao = "Não Atendidos";
                        break;
                }

                return View(listaDeSugestoes);
            }
            catch (Exception)
            {
                return View(null);
            }
        }

        public async Task<IActionResult> DetSugestao(int idSugestao, string emailUsuario)
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);
            try
            {
                //lista de sugestoes
                var listaDeSugestoes = await new SugestaoRequest().GetAllSugestoes(token);

                var sugestaoRetornar = listaDeSugestoes.Where(x => x.Id == idSugestao).FirstOrDefault();

                return View(sugestaoRetornar);
            }
            catch (Exception)
            {
                return View(null);
            }
        }


        [HttpPost]
        public async Task<IActionResult> DetSugestao(SugestaoResposta respostaSugestao)
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);

            try
            {
                var resposta = await new SugestaoRequest().EnviarRespostaSugestao(respostaSugestao, token);
                return Redirect("~/Home/Painel");

            }
            catch (Exception)
            {
                return Redirect("~/Home/Sugestoes");
            }
        }

        public async Task<IActionResult> Conta()
        {
            await Token.CheckCookies(HttpContext);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Conta(string loginNovo = "", string senhaAntiga = "", string senhaNova = "", string confirmarSenhaNova = "", string emailNovo = "")
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);

            //pega todos os dados do administrador e coloca a api
            var json = JsonConvert.SerializeObject(new { Login = User.Claims.ToList()[0].Value, Senha = User.Claims.ToList()[2].Value });
            //faz a requisição
            var administrador = await new ContaRequest().GetAdministradorByLogin(token, json);

            if (loginNovo != "")
            {
                administrador.Login = loginNovo;
                var jsonAtualizado = JsonConvert.SerializeObject(administrador);
                var resposta = await new ContaRequest().Update(token, jsonAtualizado);

                ViewBag.Info = resposta + "\nRelogue para ver os dados alterados.";
            }
            else if (senhaAntiga != "")
            {
                if (senhaAntiga != User.Claims.ToList()[2].Value)
                {
                    ViewBag.Info = "Senha antiga incorreta!";
                }
                else if (senhaNova != confirmarSenhaNova)
                {
                    ViewBag.Info = "As senhas não conferem";
                }
                else
                {
                    administrador.Senha = senhaNova;
                    var jsonAtualizado = JsonConvert.SerializeObject(administrador);
                    var resposta = await new ContaRequest().Update(token, jsonAtualizado);

                    ViewBag.Info = resposta;
                }
            }
            else if (emailNovo != "")
            {
                administrador.Email = emailNovo;
                var jsonAtualizado = JsonConvert.SerializeObject(administrador);
                var resposta = await new ContaRequest().Update(token, jsonAtualizado);

                ViewBag.Info = resposta + "\nRelogue para ver os dados alterados.";
            }
            else
            {
                ViewBag.Info = "Os dados devem ser inseridos!";
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }

        [HttpDelete]
        public async Task<string> DeletarEditora(string nomeEditora)
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);

            try
            {
                var deletarEditoraMensagem = await new EditoraRequest().Delete(nomeEditora, token);
                return deletarEditoraMensagem;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [HttpDelete]
        public async Task<string> DeletarGenero(string nomeGenero)
        {
            await Token.CheckCookies(HttpContext);
            var token = Token.GetToken(HttpContext);

            try
            {
                var deletarGeneroMensagem = await new GeneroRequest().Delete(nomeGenero, token);
                return deletarGeneroMensagem;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

    }
}
