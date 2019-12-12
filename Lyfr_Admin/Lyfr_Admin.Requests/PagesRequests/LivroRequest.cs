using Lyfr_Admin.Models.Entities;
using Lyfr_Admin.Requests.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lyfr_Admin.Requests.PagesRequests
{
    public class LivroRequest
    {
        public async Task<string> Insert(Livros livro, string token)
        {
            try
            {
                var autorSerializado = JsonConvert.SerializeObject(livro);
                var resposta = await new RequestAPI().PostApi("Livros/Insert/", token, autorSerializado);

                return resposta;
            }
            catch (Exception)
            {
                return "Ocorreu algum erro ao se comunicar com a base de dados!";
            }
        }

        public async Task<string> Update(Livros livro, string token)
        {
            try
            {
                var autorSerializado = JsonConvert.SerializeObject(livro);
                var resposta = await new RequestAPI().PutApi("Livros/Update/", token, autorSerializado);

                return resposta;
            }
            catch (Exception)
            {
                return "Ocorreu algum erro ao se comunicar com a base de dados!";
            }
        }

        public async Task<List<Livros>> GetAllLivros(string token)
        {
            try
            {
                var jsonDeLivros = await new RequestAPI().GetApi("Livros/GetAllLivros/0", token);
                var listaDeLivros = JsonConvert.DeserializeObject<List<Livros>>(jsonDeLivros);

                return listaDeLivros;
            }
            catch (Exception ex)
            {
                string ola = ex.Message;
                return null;
            }
        }

        public async Task<Livros> GetLivroByTitulo(string Titulo, string token)
        {
            try
            {
                var livroSerializado = JsonConvert.SerializeObject(Titulo);
                var jsonDeLivros = await new RequestAPI().PostApi("Livros/GetLivroByTituloWithoutFile", token, livroSerializado);
                var Livro = JsonConvert.DeserializeObject<Livros>(jsonDeLivros);

                return Livro;
            }
            catch (Exception ex)
            {
                string ola = ex.Message;
                return null;
            }
        }

        public async Task<string> Delete(string Titulo, string token)
        {
            try
            {
                var livroSerializado = JsonConvert.SerializeObject(Titulo);
                var response = await new RequestAPI().DeleteApi("Livros/DeleteByTitulo", token, livroSerializado);
                return response;
            }
            catch (Exception ex)
            {
                string ola = ex.Message;
                return null;
            }
        }
    }
}
