using Relatorios.Clientes;
using Relatorios.ClientesRelatorios;
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
        public static Relatorio CreateGetRelatorio(string empresa, string tipo )
        {
            IRepositorio repository = RepositoryFactory.CreateRepository(tipo);

            //if (empresa == "StaMaria")
            //{
            //    Relatorio getRelatorio = new StaMaria(repository);
            //    return getRelatorio;
            //}
            
            if (empresa == "Cedetec")
            {
                Relatorio getRelatorio = new Cedetec(repository);
                return getRelatorio;
            }
            
            //if(empresa == "Gorilla")
            //{
            //    Relatorio getRelatorio = new Gorilla(repository);
            //    return getRelatorio;
            //}

            //if (empresa == "GSI")
            //{
            //    Relatorio getRelatorio = new GSI(repository);
            //    return getRelatorio;
            //}

            return new Cedetec(repository);


        }

    }
}
