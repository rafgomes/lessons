using Relatorios.Clientes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.Dados
{
    public class ExportaRelatorio
    {
        private readonly IRelatorio relatorio;

        public ExportaRelatorio(IRelatorio relatorio)
        {
            this.relatorio = relatorio;
        }

        public void ExportaTXT()
        {
            string resultTXT = relatorio.GetRelatorio();

            Console.WriteLine(resultTXT);

           // File.WriteAllText($@"C:\Projets\Lessons\Camilo\Relatorios\DB{relatorio.Nome}_Relatorio.txt", resultTXT);

        }
    }
}
