using AulaIntefaces.AprendendoMais.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulaIntefaces.AprendendoMais
{
    public class CmdManager
    {

        List<ICommandos> commands = new List<ICommandos>();

        public void AddCmd(ICommandos command)
        {
            commands.Add(command);
        }

        public void ExecuteCmd()
        {
            foreach (ICommandos command in commands)
            {
                try
                {
                    command.ExecuteCmd();
                }
                catch (CmdExceptions)
                {
                    Console.WriteLine("Deu algum pau no cmd");

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Deu outro pau");
                }
            }
        }

    }


}
