using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula20
{
    internal class Program
    {
        /*
        sealed class Animal
        {
            public string teste;
        }
        
        class Macaco : Animal //não pode herdar uma classe selada
        {
            public string teste2()
            {
                return "sss";
            }
        }
        */

        /*
        class Animal
        {
            public string teste;
            public sealed string Teste2()
            {
                return "";
            }
        }

        class Macaco : Animal
        {
            public string Teste2() //tambem não pode ser sobrescrito um metodo selado
            {
                return "sss";
            }
        }
        */

        /*
        partial class Animal //classe partial uma faz parte da outra
        {
            public string teste;
            partial void tt();
        }

        partial class Animal //quando instanciar a classe Animal, terá acesso a todos os metodos e atributos
        {
            public string teste2;
            partial void tt()
            {
                Console.WriteLine("teste");
            }
        }
        */





        static void Main(string[] args)
        {
            Email.Instancia.Corpo = "O corpo do email aqui";
            Email.Instancia.Titulo = "TITULO DO EMAIL";
            Email.Instancia.Origin = "origem@email.com";
            Email.Instancia.Destino = "destion@email.com";

            Email.Instancia.EnviarEmail();
        }
    }
}
