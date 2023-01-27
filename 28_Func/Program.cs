using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace _28_Func
{
    internal class Program
    {
        delegate float defFunDelegate(int p1, int p2);
        //delegate TResult defFunDelegateGenerics<in T1, in T2, out TResult>(T1 p1, T2 p2); //Delegate Generics, entre os sinais <> especifica os tipos "in" entrada, "out" saída
        //Com a função FUNC não é necessario o delegate acima

        static void Main(string[] args)
        {
            float media;

            //defFunDelegateGenerics<int, int, float> f1; //variavel do Delegate Generics
            Func<int, int, float> f1;
            f1 = CalculaMedia;
            media = f1(5, 6);

            Console.WriteLine($"O valor da média é: {media}");
            Console.ReadKey();
 
        }

        static float CalculaMedia(int a, int b)
        {
            return (a + b) / 2;
        }
    }
}
