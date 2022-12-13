using Relatorios.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.Dados
{
    public class ExportRelatorios
    {
        public void ExportarRelatorios(List<IRelatorio> relatorios)
        {
            foreach(var relatorio in relatorios)
            {
                var exportRelatorio = new ExportaRelatorio(relatorio);
                exportRelatorio.ExportaTXT();
            }
        }

    }
}
