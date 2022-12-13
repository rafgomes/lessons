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

            if (empresa == "GSI")
            {
                IRelatorio getRelatorio = new GSI();
                return getRelatorio;
            }

            return new Cedetec(repository);


        }

    }
}
