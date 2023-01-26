using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modificadores
{
    class Animal
    {
        private int idade = 10;
        double peso = 50.4; //por padrão ela tambem é privada
        public double peso2 = 25.2;

        public int GetIdade()
        {
            return idade;
        }

        public double ObeterPeso
        {
            get { return peso; }
        }
    }
}
