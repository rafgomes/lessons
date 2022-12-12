using Aula17.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Aula17Interface
{
    internal class Program //Corrigindo a aula 14 com Polimorfismo
    {
        static void Main(string[] args)
        {
            Console.WriteLine("======Cadastro De Clientes=====");
            Cliente c = new Cliente();
            c.Nome = "Pedro";
            c.Telefone = "11988887777";
            c.CPF = "12345678910";
            c.Gravar();

            foreach (Cliente c1 in new Cliente().Ler())
            {
                Console.WriteLine(c1.Nome);
                Console.WriteLine(c1.Telefone);
                Console.WriteLine(c1.CPF);
            }

            Console.ReadLine();
        }


    }

}
