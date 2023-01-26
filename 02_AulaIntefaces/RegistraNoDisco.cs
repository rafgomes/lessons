using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AulaIntefaces
{
    class RegistraNoDisco : IRegistra{

        private string NomeArquivo { get; set; }

        public RegistraNoDisco(string nomeArquivo)
        {
            this.NomeArquivo = nomeArquivo;
        }

        public void RegistraInfo(string mensagem)
        {
            using (StreamWriter arquivo = new StreamWriter(NomeArquivo))
            {
                arquivo.WriteLine(mensagem);
            }
        }

    }

}
