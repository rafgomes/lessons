using FuncoesRecursivas.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncoesRecursivas.Funcoes
{
    public class ClassCadastrarClientes
    {
        public void CadastrarClientes()
        {
            var clientes = new Cliente();

            Console.WriteLine("=============CADASTRAR CLIENTES=============");
            Console.WriteLine("Digite o nome:");
            clientes.Nome = Console.ReadLine();
            Console.WriteLine("Digite o telefone:");
            clientes.Telefone = Console.ReadLine();
            Console.WriteLine("Digite o CPF:");
            clientes.CPF = Console.ReadLine();

            Cliente.Gravar();
        }
    }
}
