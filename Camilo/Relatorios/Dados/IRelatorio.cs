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
        //public string Nome { get; set; }
        //public string Idade { get; set; }
        //public string Telefone { get; set; }
        //public string Endereco { get; set; }
        
        string GetRelatorio(string empresa);
    }
}
