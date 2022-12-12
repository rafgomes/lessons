using Aula17.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula17
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

            foreach (Base c1 in new Cliente().Ler())
            {
                Console.WriteLine(c1.Nome);
                Console.WriteLine(c1.Telefone);
                Console.WriteLine(c1.CPF);
                Console.WriteLine("===============================");
            }

            Console.WriteLine("======Cadastro De Usuarios=====");
            Usuario u = new Usuario();
            u.Nome = "José";
            u.Telefone = "000000000";
            u.CPF = "55566677722200";
            u.Gravar();

            foreach(Base u1 in new Usuario().Ler())
            {
                Console.WriteLine(u1.Nome);
                Console.WriteLine(u1.Telefone);
                Console.WriteLine(u1.CPF);
                Console.WriteLine("===============================");
            }

            Console.ReadLine();
        }


    }
}
