using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _30_AsyncAwait
{
    internal class Program
    {
        static async Task PassarCafe()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"Passando Café {i}");
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Café Pronto!");
            });

        }

        static void TostarPao()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Tostando o Pão {i}");
                Thread.Sleep(500);
            }
            Console.WriteLine("Pão Pronto!");
        }

        static void Main(string[] args)
        {
            Task taskPassarCafe = PassarCafe();
            TostarPao();
            taskPassarCafe.Wait();
            Console.WriteLine("CAFÉ NA MESA!");
            Console.ReadKey();
        }
    }
}
