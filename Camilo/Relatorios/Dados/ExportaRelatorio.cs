using Relatorios.Clientes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.Dados
{
    public class ExportaRelatorio : IRelatorio
    {
        private readonly IRelatorio getRelatorio;

        public ExportaRelatorio(IRelatorio getRelatorio)
        {
            this.getRelatorio = getRelatorio;
        }

        public void ExportaTXT(string empresa)
        {
            string resultTXT = getRelatorio.GetRelatorio(empresa);

            File.WriteAllText($@"C:\Projets\Lessons\Camilo\Relatorios\DB{empresa}_Relatorio.txt", resultTXT);

        }
    }
}
