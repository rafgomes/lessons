using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulaIntefaces
{
    internal class RegistraOcorrencia 
    {
        #region FormaIncorreta
        //utilizando forma icorreta com Classe, é melhor trabalhar utilizando interface
        //public void RegistraConsole(string mensagem)
        //{
        //    Console.WriteLine(mensagem);
        //    Console.ReadKey();
        //}

        //public void RegistraNoDisco(string nomeArquivo, string mensagem)
        //{
        //    using (StreamWriter aqrquivo = new StreamWriter(nomeArquivo))
        //    {
        //        aqrquivo.WriteLine(mensagem);
        //    }

        //}
        #endregion

        private IRegistra Registra;

        public RegistraOcorrencia(IRegistra registra)       //pode passar tanto RegistraConsole como RegistraNoDisco 
        {                                                   //Injeção de depndencia.
            Registra = registra;
        }

        public void Registrar(string mensagem)
        {
            Registra.RegistraInfo(mensagem);
        }

    }
}
