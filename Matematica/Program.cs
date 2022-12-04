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
            Matematica matematica = new Matematica(); //correto

            //Matematica.Soma(1, 3); //nao funciona pq o metodo nao é static  compartilhado com os objtos
           // var console = new Console(); //nao deixa criar instancia pq eh um classe static


            Console.WriteLine("Digite o Primeiro Numero: "); //correto

           // string retornoString = Console.ReadLine(); //armazena o retorno do método ReadLine, nao precisou criar um objeto de Console pq o metodo ReadLine é static

            int a = int.Parse(Console.ReadLine()); // variavel a armazena o RETORNO do método 
            matematica.num1 = a;

            Console.WriteLine("Digite o Segundo Numero: ");
            int b = int.Parse(Console.ReadLine());
            matematica.num2 = b;

            matematica.Teste = "Valor original";

            int resultadoDaSoma = matematica.Soma(1, 2);

            //armazena o resultado da subtracao passando os parametros 
            //armazena o resultado da multi passando parametros
            //armazena o resultado da divisao e salva aqui

            Console.WriteLine("");
            Console.WriteLine($"Soma: {resultadoDaSoma}");
            Console.WriteLine($"Subtração: {matematica.Sub()}");
            Console.WriteLine($"Multiplicação: {matematica.Mult()}");
            Console.WriteLine($"Divisão: {matematica.Div()}");



            Console.ReadLine();
        }
    }
}
