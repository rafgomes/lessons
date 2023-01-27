using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _27_Delegate //Delegate é um recurso que permite criar uma variavel que aponta para um método
{
    internal class Program
    {
        delegate int Funcao(int a, int b); //especificando os parametros do delegate
        
        static void Main(string[] args)
        {
            int resultado;
            //Funcao funcao = new Funcao(CalculaMedia); //cria a variavel que vai apontar para a função
            Funcao funcao = CalculaMedia; //novo metodo mais facil para dar um "new" 
            resultado = funcao(8, 4); //está chamando o metodo atraves da variavel
            Console.WriteLine($"Media: {resultado}");


            funcao = new Funcao(CalculaMulti); //a mesma variavel pode chamar dois metodos (tendo a mesma assinatura)
            resultado = funcao(4, 6);
            Console.WriteLine($"Resultado: {resultado}");


            Console.ReadKey();
        }

        static int CalculaMedia(int p1, int p2)
        {
            int media;
            media = (p1 + p2) / 2;
            return media;
        }

        static int CalculaMulti(int p1, int p2)
        {
            int resMulti;
            resMulti = p1 * p2;
            return resMulti;
        }

        
    }
}
