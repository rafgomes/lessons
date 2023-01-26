using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulaIntefaces.AprendendoMais.Commandos
{
    class SendEmailFilesCmd : ICommandos
    {
        public void ExecuteCmd()
        {
            Console.WriteLine("Send Email");
        }
    }
}
