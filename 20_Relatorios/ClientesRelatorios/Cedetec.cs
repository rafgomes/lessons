using Relatorios.ClientesRelatorios;
using Relatorios.Dados;
using Relatorios.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.Clientes
{
    public class Cedetec : Relatorio
    {
        private readonly IRepositorio repository;   

        public Cedetec(IRepositorio repository) {
            this.repository = repository;
            base.Nome = "Cedetec";
        }

        //public string Nome { get; set; } = "Cedetec";

        public string GetRelatorio2()
        {
           
            var clients = repository.GetClients(Nome);
            //StringBuilder stringBuilder= new StringBuilder();

            //stringBuilder.AppendLine(            )


            //List<string> lines = new List<string>();

            //foreach (var client in clients)
            //{
            //     string line = $"{client.Nome},{client.Idade}";

            //    lines.Add(line);
            //}
            //string result = String.Join(Environment.NewLine, lines);


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
