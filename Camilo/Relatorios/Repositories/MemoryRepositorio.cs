using Relatorios.Clientes;
using Relatorios.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.Dados
{
    public class  MemoryRepositorio : IRepositorio
    {
        public List<Cliente> GetClients(string empresa)
        {
            Console.WriteLine($"Pegando clientes da memoria");

            //Ideal seria fazer um $"select nome, idade, endereco, telefone from Clientes where Empresa = '{empresa}'" 
            //using (db = naosei.createContin())
            //bal bal bal
            // white(reader.read()){
            ////////
            ////

            var clients = new List<Cliente>();
            clients.Add(new Cliente()
            {
                Endereço = "Rua",
                Idade = "idade 2",
                Nome = "nome 2",
                Telefone = "1234134"
            });
            clients.Add(new Cliente()
            {
                Endereço = "Rua3",
                Idade = "idade 5",
                Nome = "nome 66",
                Telefone = "11111"
            });


            return clients;
        }

    }
}
