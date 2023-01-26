using AulaIntefaces.AprendendoMais.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulaIntefaces.AprendendoMais.Commandos
{
    public class CopyFilesCmd : ICommandos
    {
        string toFolder;
        public CopyFilesCmd(string toFolder)
        {
            this.toFolder = toFolder;
        }

        public void ExecuteCmd()
        {
            if(Directory.Exists(toFolder)) 
            {
                Console.WriteLine($"Copiando para pasta {toFolder}");
            }
            else
            {
                throw new CmdExceptions(); //SE O DIRETORIO NAO EXISTIR LANÇA UMA EXCECAO
            }
        }
    }
}
