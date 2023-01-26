using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.ClientesRelatorios
{
    public class Relatorio
    {
        public string Nome { get; set; }
        public virtual string GetRelatorio()
        {
            return Nome;
        }
    }
}
