using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matematica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Matematica matematica = new Matematica(); //cria objeto 'matematica' do tipo 'Matematica'

            Console.WriteLine("Primeiro Numero:");
            int a = int.Parse(Console.ReadLine());

            Console.WriteLine("Segundo Valor:");
            int b = int.Parse(Console.ReadLine());

            Console.WriteLine("");

            OperacoesComplexas oc = new OperacoesComplexas();
            int resultado = oc.Soma(a, b);

            int resultado = matematica.
            
            double resultSoma = oc.SomaMultiDiv(a, b, 10, 2); //armazena o retorno da função 'Soma' na variavel 'resultSoma'
            Console.WriteLine($"Soma: {resultSoma}");
            
            int resultSub = matematica.Sub(a, b);
            Console.WriteLine($"Subtração: {resultSub}");

            int resultMulti = matematica.Mult(a, b);
            Console.WriteLine($"Multiplicação: {resultMulti}");

            double resultDiv = matematica.Div(a, b);
            Console.WriteLine($"Divisão: {resultDiv}");


            Console.ReadLine();
        }
    }
}
