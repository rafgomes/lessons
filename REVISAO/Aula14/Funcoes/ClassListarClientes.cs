using Aula14.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula14.Funcoes
{
    public class ClassListarClientes
    {
        public static void ListarClientes()
        {
            Console.WriteLine("=============LISTA DE CLIENTES=============");
            var clientes = Cliente.LerUsuarios();
            foreach (Cliente c in clientes) //lendo todos os nomes dos clientes no arquivo
            {
                Console.WriteLine($"Nome: {c.Nome}, CPF: {c.CPF}, Telefone: {c.Telefone}");

            }
            Console.WriteLine("===========================================");
            Console.ReadKey();
        }
    }
}
