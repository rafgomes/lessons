using Aula13.Classes;
using Aula13.Diretorio;
using Aula13.Funcoes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Aula13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region LerArquivos
            //Arquivos.Ler(1);
            //Console.ReadKey();
            #endregion

            #region Aula12
            /*
            //Cliente.LerClientes(); //metodo estatico, traz todos os clientes/ todos os objetos
            //Console.ReadKey();

            //Cliente c = new Cliente { Nome = "Exemplo", CPF = "123", Telefone = "123"} //exemplo de como instanciar já passando os valores dos parametros
            

            var clientes = Cliente.LerClientes();
            foreach (Cliente c in clientes) //lendo todos os nomes dos clientes no arquivo
            {
                Console.WriteLine($"Nome: {c.Nome}, CPF: {c.CPF}, Telefone: {c.Telefone}");
               
            }
            Console.ReadKey();



            //var cliente = new Cliente(); //traz todas as propriedades e metodos dessa instancia para a variavel
            //cliente.Nome = "Jose";
            //cliente.Telefone = "11222333";
            //cliente.CPF = "11122233344455";
            //cliente.Gravar();

            //var cliente2 = new Cliente();
            //cliente2.Nome = "Maria";
            //cliente2.Telefone = "1100009999";
            //cliente2.CPF = "33355577799";
            //cliente2.Gravar();
            */
            #endregion

            Menu.Criar();
        }


    }

}
