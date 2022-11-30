using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Matematica matematica = new Matematica();
            
            Console.WriteLine("Digite o Primeiro Numero: ");
            int a = int.Parse(Console.ReadLine());
            matematica.num1 = a;

            Console.WriteLine("Digite o Segundo Numero: ");
            int b = int.Parse(Console.ReadLine());
            matematica.num2 = b;

            Console.WriteLine("");
            Console.WriteLine($"Soma: {matematica.Soma()}");
            Console.WriteLine($"Subtração: {matematica.Sub()}");
            Console.WriteLine($"Multiplicação: {matematica.Mult()}");
            Console.WriteLine($"Divisão: {matematica.Div()}");



            Console.ReadLine();
        }
    }
}
