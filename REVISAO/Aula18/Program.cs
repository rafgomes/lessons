using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var c = new Cachorro();
            c.Idade = 1;
            Console.WriteLine(c.Idade);

            c.Idade2 = 3; 
            Console.WriteLine(c.Idade2);
        }
    }
}
