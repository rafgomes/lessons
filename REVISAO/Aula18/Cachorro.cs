using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula18
{
    public class Cachorro : Animal
    {
        public int Idade; //propriedade/atributo simples
        public int Idade3 { get; } //variavel somente de leitura

        private int IdadePreDefinida = 2; //pre definida
        public int Idade2
        {
            get
            {
                return IdadePreDefinida;
            }
            set //passa a valer essa idade setada não a pre definida
            {
                Idade = value;
            }
        }


        public override void Latir()
        {
            Console.WriteLine("Au Au AU");
        }
    }
}
