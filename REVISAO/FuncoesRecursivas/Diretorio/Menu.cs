using FuncoesRecursivas.Classes;
using FuncoesRecursivas.Funcoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncoesRecursivas.Diretorio
{
    public class Menu
    {
        public const int LISTAR_CLIENTES = 0;
        public const int CADASTRAR_CLIENTES = 1;
        public const int SAIR = 2;

        public static void Criar()
        {
            while (true)
            {
                string mensagem = "Olá, bem vindo ao programa!\n" +
                    "\n Digite uma das opções:" +
                    "\n 0 - Listar Clientes" +
                    "\n 1 - Cadastrar Cliente" +
                    "\n 2 - SAIR";
                Console.WriteLine(mensagem);

                int valor = int.Parse(Console.ReadLine());

                if (valor == SAIR)
                {
                    break;
                }
                else if (valor == LISTAR_CLIENTES)
                {
                    ClassListarClientes.ListarClientes();
                }
                else if (valor == CADASTRAR_CLIENTES)
                {
                    ClassCadastrarClientes.CadastrarClientes();
                }
                else
                {
                    Console.WriteLine("Opção inválida, digite novamente!");
                }
            }

        }
    }
}
