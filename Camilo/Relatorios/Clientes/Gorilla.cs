using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.Clientes
{
    public class Gorilla : IRelatorio
    {
        public Gorilla(string nome, string idade, string telefone, string endereco)
        {
            nome = "Quintal Do Gorilla";
            idade = "2";
            telefone = "1638390000";
            endereco = "Av Portugal";
        }
    }
}
