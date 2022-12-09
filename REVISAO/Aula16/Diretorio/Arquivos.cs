using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Aula16Interface.Diretorio
{
    class Arquivos
    {
        private static string CaminhoArquivo()
        {
            return ConfigurationManager.AppSettings["caminho_arquivos"];
        }
        public static void Ler(int numeroArquivo)
        {
            string arquivoComCaminho = CaminhoArquivo()  + "Arq" + numeroArquivo + ".txt";
            Console.WriteLine("========== Lendo Arquivo==============\n" + arquivoComCaminho + "================================");

            if (File.Exists(arquivoComCaminho))
            {
                using (StreamReader arquivo = File.OpenText(arquivoComCaminho))
                {
                    string linha;
                    while ((linha = arquivo.ReadLine()) != null)
                    {
                        Console.WriteLine(linha);
                    }
                }
            }

            string arquivoComCaminho2 = CaminhoArquivo() + "Arq" + (numeroArquivo + 1) + ".txt";
            if (File.Exists(arquivoComCaminho2))
            {
                Arquivos.Ler(numeroArquivo + 1);
            }
            
        }

    }
}
