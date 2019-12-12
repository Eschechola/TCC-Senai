using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lyfr_Admin.Files
{
    public class FilesManipulation
    {
        private string imagemNotFound;

        public FilesManipulation()
        {
            imagemNotFound = "imgsAutores/NotFound/NotFound.png";
        }

        public string GerarNomeImagem()
        {
            //inicia string em branco
            string nomeArquivo = string.Empty;

            //define o nome do arquivo como data (para não haver repetição)
            nomeArquivo = DateTime.Now.ToString();

            //retira os caracteres especiais
            nomeArquivo = nomeArquivo.Replace("/", "_");
            nomeArquivo = nomeArquivo.Replace(":", "_");
            nomeArquivo = nomeArquivo.Replace(" ", "_");

            return nomeArquivo;
        }

        public string ConverterArquivoBase64(string diretorioArquivo)
        {
            try
            {
                //le todos os bytes do arquivo que está no diretorio informado
                byte[] bytesDoArquivo = File.ReadAllBytes(diretorioArquivo);
                //converte esses bytes para a base64
                string arquivoBase64 = Convert.ToBase64String(bytesDoArquivo);
                //retorna a base64 em formato de stringg
                return arquivoBase64.ToString();
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return "";
            }
        }

        public string SalvarFoto(string diretorioRaiz, string pastaArquivo, IFormFileCollection arquivos)
        {
            try
            {
                foreach (var arquivo in arquivos)
                {
                    if (arquivo.Length > 0)
                    {
                        //define o nome do arquivo como um novo
                        var nomeArquivo = GerarNomeImagem();

                        // concatena nomeArquivo + extensão
                        nomeArquivo += ".jpg";

                        // combina o diretorio do arquivo + diretorio
                        var diretorioDeArmazenamento = Path.Combine(diretorioRaiz, pastaArquivo);
                        if (!Directory.Exists(diretorioDeArmazenamento))
                        {
                            Directory.CreateDirectory(diretorioDeArmazenamento);
                        }
                        var diretorioCompleto = Path.Combine(diretorioDeArmazenamento, nomeArquivo);
                        //copia a foto enviada e cola no diretório de armazenamento informado
                        using (FileStream streamDeDados = File.Create(diretorioCompleto))
                        {
                            arquivo.CopyTo(streamDeDados);
                            streamDeDados.Flush();
                        }

                        //retorna o diretório de onde esse arquivo foi guardado
                        return diretorioCompleto;
                    }
                    else
                    {
                        //caso envie uma imagens vazia, ele envia a imagem notFound
                        return Path.Combine(diretorioRaiz, imagemNotFound);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return Path.Combine(diretorioRaiz, imagemNotFound);
        }
    }
}
