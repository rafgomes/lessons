using Relatorios.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.Dados
{
    public class GerarRelatorio
    {
        public static IRelatorio CreateGetRelatorio(string empresa)
        {
            if(empresa == "StaMaria")
            {
                IRelatorio getRelatorio = new DBRepositorio();
                return getRelatorio;
            }
            
            if (empresa == "Cedetec")
            {
                IRelatorio getRelatorio = new DBRepositorio();
                return getRelatorio;
            }
            
            if(empresa == "Gorrila")
            {
                IRelatorio getRelatorio = new DBRepositorio();
                return getRelatorio;
            }

            return new DBRepositorio();
            
        }

    }
}
