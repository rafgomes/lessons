using Relatorios.Dados;
using Relatorios.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.Repositories
{
    public class FileRepository : IRepositorio
    {
        public List<Cliente> GetClients(string empresa)
        {
            List<Cliente> clientes = new List<Cliente>();

            try
            {
                string result = File.ReadAllText($@"C:\temp\{empresa}.txt");
                string[] lines = result.Split('\n');
                foreach (string line in lines)
                {
                    string[] columns = line.Split(';');

                    string nome = columns[0];
                    string idade = columns[1];
                    string telefone = columns[2];
                    string endereco = columns[3];

                    Cliente cliente = new Cliente();
                    cliente.Nome = nome;
                    cliente.Idade = idade;
                    cliente.Endereço = endereco;
                    cliente.Telefone = telefone;

                    clientes.Add(cliente);
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Cliente nao encontrado");
                return clientes;               
            }      
          
            return clientes;

        }
    }
}
