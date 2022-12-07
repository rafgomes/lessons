using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AulaIntefaces.AprendendoMais.Commandos
{
    public class MoveArquivoCmd : ICommandos
    {
        string toFolder;
        public MoveArquivoCmd(string toFolder)
        {
            toFolder = toFolder.ToLower();
        }

        public void ExecuteCmd()
        {
            Console.WriteLine($"Movendo arquivos para {this.toFolder}");
        }
    }
}
