using Relatorios.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.Dados
{
    internal class DBRepositorio : IRelatorio
    {
        public string GetRelatorio(string empresa)
        {
            Console.WriteLine($"Fazendo request no TXT para o cliente:{empresa}");

            return empresa;
        }

    }
}
