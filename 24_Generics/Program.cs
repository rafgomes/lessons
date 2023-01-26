using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int i1 = 2;
            int i2 = 2;

            float f1 = 2;
            float f2 = 2;

            string s1 = "2";
            string s2 = "2";

            //Compara(s1, s2);

            ClassCompara<int, int> obj1 = new ClassCompara<int, int>(); //cria um objeto com parametros inteiros
            obj1.Compara(i1, i2);

            ClassCompara<float, float> obj2 = new ClassCompara<float, float>(); //tem que definir o tipo do objeto
            obj2.Compara(i2, i1);

            List<string> ls; //o list por padrão tambem já utiliza o  Generics
            List<int> li; 

            Console.ReadKey();
        }

        //static void Compara<TipoA>(TipoA p1, TipoA p2) //utilizando o Generics informa o tipo
        //{
        //    bool resultado;
        //    resultado = p1.Equals(p2);
        //    Console.WriteLine(resultado);
        //}

        //static void Compara<TipoA, TipoB>(TipoA p1, TipoB p2) //pode comparar tambem dois tipos
        //{
        //    bool resultado;
        //    resultado = p1.Equals(p2);
        //    Console.WriteLine(resultado);
        //}

        //static void Compara(int p1, int p2)
        //{
        //    bool resultado;
        //    resultado = p1.Equals(p2); //metodo Equals compara o valor e o tipo
        //    Console.WriteLine($"Int = {resultado}");
        //}

        //static void Compara(float p1, float p2)
        //{
        //    bool resultado;
        //    resultado = p1.Equals(p2);
        //    Console.WriteLine($"Float = {resultado}");
        //}

        //static void Compara(string p1, string p2)
        //{
        //    bool resultado;
        //    resultado = p1.Equals(p2);
        //    Console.WriteLine($"String = {resultado}");
        //}
    }
}
