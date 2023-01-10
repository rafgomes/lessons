using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramacaoAssincrona
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Pressione uma tecla para inicar...\n");
            Console.ReadKey();

            await Aguardar(5);

            Console.WriteLine("Finalizado os 5 segundos!\n");
            Console.ReadKey();

                                 
        }

        static async Task Aguardar(int tempo)
        {
            Console.WriteLine("Iniciando espera...");
            await Task.Delay(TimeSpan.FromSeconds(tempo));
            Console.WriteLine("Fim da espera!");
        }
    }
}
