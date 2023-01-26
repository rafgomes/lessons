using Relatorios.Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.Clientes
{
    public class StaMaria : IRelatorio
    {
        public string Nome { get; set; } = "StaMaria";

        private readonly IRepositorio repository;

        public StaMaria(IRepositorio repository)
        {
            this.repository = repository;
        }

        public string GetRelatorio()
        {

            var clients = repository.GetClients(Nome);
            
            StringBuilder builder = new StringBuilder();
            foreach (var client in clients)
            {
                string line = $"{client.Nome},{client.Idade}";
                builder.AppendLine(line);

            }

            string result = builder.ToString();


            return result;


        }

        
    }
}
