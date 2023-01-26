using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matematica
{
    public class Matematica
    {
        protected int Soma(int snum1, int snum2)
        {
            int resultSoma = snum1 + snum2;
            return resultSoma;
        }
        public int Soma(int paramtero1, int paramtero2)
        {
            int result = paramtero1 + paramtero2; //esse result altera apenas nesse scopo nao mexe no escopo da classe
            // result ou; this.result = 1 + 2; altera o escopo do objeto 

             this.Teste = "alterei o valor"; //aqui vai alterar o valor do objeto dessa classe
             String Teste = "outro valor"; //aqui vai alterar o valor dessa variavel desse escopo do metodo Soma
 
            Console.WriteLine(Teste);
            
            return result;
        }

        protected int Soma(int snum1, int snum2, int multi)
        {
            int resultSoma = Soma(snum1, snum2);
            return resultSoma * multi;
        }

        public int Sub(int subn1, int subn2)
        {
            return subn1 - subn2;
        }

        public int Mult(int multn1, int multn2)
        {
            return multn1 * multn2;
        }

        public double Div(double divn1, double divn2)
        {
           if(divn1== 0 || divn2 == 0) 
           {
                return 0;
           }
            else
            {
                return divn1 / divn2;
            }
            
        }
    }
}
