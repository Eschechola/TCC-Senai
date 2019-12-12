using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lyfr_Admin.Files
{
    public class FilesManipulation
    {
        public string SalvarFoto(string diretorioRaiz, string pastaArquivo, IFormFileCollection arquivos)
        {
            try
            {
                foreach (var arquivo in arquivos)
                {
                    if (arquivo.Length > 0)
                    {
                        //define o nome do arquivo como a hora atual
                        var nomeArquivo = DateTime.Now.ToString();

                        //retira os caracteres especiais
                        nomeArquivo = nomeArquivo.Replace("/", "_");
                        nomeArquivo = nomeArquivo.Replace(":", "_");
                        nomeArquivo = nomeArquivo.Replace(" ", "_");

                        // concatena nomeArquivo + extensão
                        nomeArquivo = nomeArquivo + ".jpg";

                        // combina o diretorio do arquivo + diretorio
                        var diretorioDeArmazenamento = Path.Combine(diretorioRaiz, pastaArquivo, nomeArquivo);

                        //copia a foto enviada e cola no diretório de armazenamento informado
                        using (FileStream streamDeDados = File.Create(diretorioDeArmazenamento))
                        {
                            arquivo.CopyTo(streamDeDados);
                            streamDeDados.Flush();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return "";
        }
    }
}
