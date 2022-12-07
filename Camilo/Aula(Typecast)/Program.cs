using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulaTypecast //Typecast, é a conversão de um tipo em outro. É necessaria quando a conversão não é implicita.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n1 = 10;
            float n2 = n1;  //Conversão implicita (segura), o compilador faz a conversão automaticamente.

            Console.WriteLine(n2);
            

            float n3 = 10.5f; 
            int n4 = (int)n3; //não é possivel fazer uma operação de conversão implicita, é necessario fazer um cast

            Console.WriteLine(n4);
            Console.ReadLine();



        }
        
   
    }

}
