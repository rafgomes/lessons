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
<<<<<<< HEAD
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
=======
        public static List<IRelatorio> CreateRelatorios(string tipo)
        {
            string[] names = new string[] { "StaMaria", "Cedetec", "Gorrila", "GSI" };
            List<IRelatorio> relatorios = new List<IRelatorio>();
            foreach (string name in names)
            {
                var relatorio = CreateRelatorio(name, tipo);
                relatorios.Add(relatorio);
            }
            return relatorios;
        }

        public static IRelatorio CreateRelatorio(string empresa, string tipo)
        {
            IRepositorio repository = RepositoryFactory.CreateRepository(tipo);

            if (empresa == "StaMaria")
            {
                IRelatorio getRelatorio = new StaMaria();
                return getRelatorio;
            }

            if (empresa == "Cedetec")
            {
                IRepositorio fileRepository = new FileRepository();
                IRelatorio getRelatorio = new Cedetec(fileRepository);
                return getRelatorio;
            }

            if (empresa == "Gorrila")
            {
                IRelatorio getRelatorio = new StaMaria();
                return getRelatorio;
            }
>>>>>>> 25f17c5016f35feb7e1213f1f00b6424ae009338

            //if (empresa == "GSI")
            //{
            //    Relatorio getRelatorio = new GSI(repository);
            //    return getRelatorio;
            //}

            return new Cedetec(repository);


        }

    }
}
