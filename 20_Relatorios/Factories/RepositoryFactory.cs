using Relatorios.Clientes;
using Relatorios.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.Dados
{
    public class RepositoryFactory
    {
        public static IRepositorio CreateRepository(string tipo )
        {
            

            if(tipo == "File")
            {
                IRepositorio repository = new FileRepository();
                return repository;
            }


            return new FileRepository(); ;

        }

    }
}
