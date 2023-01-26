using Relatorios.Dados;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.Clientes
{
    public interface IRelatorio
    {
        string Nome { get; set; }
        string GetRelatorio();
    }
}
