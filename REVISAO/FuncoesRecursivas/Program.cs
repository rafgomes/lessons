using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncoesRecursivas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LerArquivo(@"C:\temp\Arq1.txt");
        }

        private static void LerArquivo(string nomeArquivo)
        {
            using (StreamReader arquivo = File.OpenText(nomeArquivo))
            {
                string linha;
                while ((linha = arquivo.ReadLine()) != null)
                {
                    Console.WriteLine(linha);
                }
            }

            Console.ReadKey();
        }


    }

}
