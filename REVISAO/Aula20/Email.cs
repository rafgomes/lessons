using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula20
{
    public class Email //classe Singleton
    { 
        private Email()
        {

        }

        private static Email instancia; //atributo instancia privado

        public string Origin;
        public string Destino; //todos os atributos publicos para fazer com que a classe Email funcione
        public string Titulo;
        public string Corpo;

        public void EnviarEmail()
        {
            Console.WriteLine($"Enviado email para: {Destino}, com o titulo: {Titulo}, menssagem: {Corpo}");
        }

        public static Email Instancia //propriedade instancia publica
        {
            get
            {
                if (instancia == null) //se instancia está vazia(null)
                {
                    instancia = new Email(); //então cria uma nova instancia
                }
                return instancia;
            }
        }
    
    }

}
