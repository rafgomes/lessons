using AulaIntefaces.AprendendoMais;
using AulaIntefaces.AprendendoMais.Commandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulaIntefaces
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region FormaINcorreta
            //RegistraOcorrencia reg = new RegistraOcorrencia();
            //reg.RegistraConsole("Texto 1");
            //reg.RegistraNoDisco(@"C:\temp\log.txt", "Este registro foi salvo no disco!");
            #endregion

            var cmdManager = new CmdManager();
            cmdManager.AddCmd( new MoveArquivoCmd(@"c:\temp2\") );
            cmdManager.AddCmd(new MoveArquivoCmd(@"c:\temp\"));
            cmdManager.AddCmd(new SendEmailFilesCmd());
            cmdManager.AddCmd(new CopyFilesCmd(@"C:\temp"));
            cmdManager.AddCmd(new DeleteArquivosCmd(@"C:\temp"));


            cmdManager.ExecuteCmd();





            //RegistraOcorrencia regConsole = new RegistraOcorrencia(new RegistraConsole());
            //regConsole.Registrar("Mensagem No Console!");

            //RegistraOcorrencia regDisco = new RegistraOcorrencia(new RegistraNoDisco(@"C:\temp\log.txt"));
            //regDisco.Registrar("Mensagem No Disco!!");


            Console.ReadKey();
        }

    }
}
