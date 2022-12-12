using Aula15.Classes;
using Aula15.Diretorio;
using Aula15.Funcoes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Aula15
{
    internal class Program //Corrigindo a aula 14 com Polimorfismo
    {
        static void Main(string[] args)
        {
            //Menu.Criar();

            Usuario u = new Usuario();
            u.Nome = "Usuario1";
            u.Telefone = "111111111";
            u.CPF = "222222222";
            u.Gravar();

            foreach(Usuario us in Usuario.LerUsuarios())
            {
                Console.WriteLine(us.Nome);
                Console.WriteLine(us.Telefone);
                Console.WriteLine(us.CPF);
            }


            Cliente c = new Cliente();
            c.Nome = "Cliente1";
            c.Telefone = "9999999999";
            c.CPF = "0000000000";
            c.Gravar();

        }


    }

}
