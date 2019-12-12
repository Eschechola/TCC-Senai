using LyfrAPI.Context;
using LyfrAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LyfrAPI.Models.ModelsDatabase;

namespace LyfrAPI.Aplicacoes.Aplicacoes
{
    public class FavoritosAplicacao
    {
        private LyfrDBContext _context;

        public FavoritosAplicacao(LyfrDBContext context)
        {
            _context = context;
        }


        public string Insert(Favoritos favoritos)
        {
            try
            {
                if (favoritos != null)
                {
                    _context.Add(favoritos);
                    _context.SaveChanges();

                    return "Favoritos cadastrado com sucesso!";

                }
                else
                {
                    return "Favoritos é nulo! Por - favor preencha todos os campos e tente novamente!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public List<Livros> GetFavoritosByUsuario(int idUsuario)
        {
            try
            {
                //realiza uma query no banco de favoritos onde o fkidcliente seja igual ao id do usuario
                //retornando uma lista de favoritos
                var queryNoBanco = from f in _context.Favoritos
                                   join c in _context.Cliente on f.FkIdCliente equals c.IdCliente
                                   where idUsuario.Equals(f.FkIdCliente)
                                   select new Favoritos
                                   {
                                       Id_Favoritos = f.Id_Favoritos,
                                       FkIdCliente = f.FkIdCliente,
                                       FkIdLivro = f.FkIdLivro
                                   };

                //lista de livros que será retornada
                var listaDeLivros = new List<Livros>();

                //adiciona os livros de acordo com o fkidlivros na lista de favoritos
                foreach(var item in queryNoBanco.ToList())
                {
                    listaDeLivros.Add(_context.Livros.Where(x => x.IdLivro.Equals(item.FkIdLivro)).ToList().FirstOrDefault());
                }

                return listaDeLivros;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string Delete(int idCliente, int idLivro)
        {
            try
            {
                if (idLivro < 0 || idCliente < 0)
                {
                    
                    return "Insira um ID válido";

                }
                else
                {
                    
                    var favoritoPegar = _context.Favoritos.Where(x => x.FkIdLivro.Equals(idLivro) && x.FkIdCliente.Equals(idCliente)).ToList().FirstOrDefault();

                    if (favoritoPegar != null)
                    {
                        _context.Favoritos.Remove(favoritoPegar);
                        _context.SaveChanges();

                        var livro = new LivrosAplicacao(_context).GetById(idLivro);
                        return "O livro de id " + livro.Titulo + " foi deletado com sucesso da sua lista";
                    }
                    else
                    {
                        return "Nenhum livro encontrado na sua lista!";
                    }
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }
    }
}
