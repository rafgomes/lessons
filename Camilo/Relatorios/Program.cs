using Relatorios.Clientes;
using Relatorios.Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios
{
    public class Program
    {
        static void Main(string[] args)
        {
            var client = args[0];
            string tipo = "File";

            if( args.Length> 1 )
            {
                tipo = args[1];
            }

            var relatorios = RelatorioFactory.CreateRelatorios(tipo);
            var exportRelatorios = new ExportRelatorios();
            exportRelatorios.ExportarRelatorios(relatorios);




            //IRelatorio relatorio = RelatorioFactory.CreateGetRelatorio("Goroliza", tipo);

            //var exportRelatorio = new ExportaRelatorio(relatorio);
            //exportRelatorio.ExportaTXT();

            //IRelatorio relatorio1 = RelatorioFactory.CreateGetRelatorio("Cedetec", tipo);

            //var exportRelatorio2 = new ExportaRelatorio(relatorio);
            //exportRelatorio.ExportaTXT();



        }
    }
}
