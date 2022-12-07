using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulaIntefaces
{
    class RegistraConsole : IRegistra{

        public void RegistraInfo(string mensagem)
        {
            Console.WriteLine("Mensagem No Console");
        }
    }
}
