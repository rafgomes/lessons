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

            RegistraOcorrencia regConsole = new RegistraOcorrencia(new RegistraConsole());
            regConsole.Registrar("Mensagem No Console!");

            RegistraOcorrencia regDisco = new RegistraOcorrencia(new RegistraNoDisco(@"C:\temp\log.txt"));
            regDisco.Registrar("Mensagem No Disco!!");


            Console.ReadKey();
        }

    }
}
