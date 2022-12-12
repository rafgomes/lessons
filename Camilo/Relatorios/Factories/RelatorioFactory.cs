using Relatorios.Clientes;
using Relatorios.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.Dados
{
    public class RelatorioFactory
    {
        public static IRelatorio CreateGetRelatorio(string empresa, string tipo )
        {
            IRepositorio repository = RepositoryFactory.CreateRepository(tipo);

            if (empresa == "StaMaria")
            {
                IRelatorio getRelatorio = new StaMaria();
                return getRelatorio;
            }
            
            if (empresa == "Cedetec")
            {
                IRelatorio getRelatorio = new Cedetec(repository);
                return getRelatorio;
            }
            
            if(empresa == "Gorrila")
            {
                IRelatorio getRelatorio = new StaMaria();
                return getRelatorio;
            }

            if (empresa == "GSI")
            {
                IRelatorio getRelatorio = new GSI();
                return getRelatorio;
            }

            return new Cedetec(repository);


        }

    }
}
