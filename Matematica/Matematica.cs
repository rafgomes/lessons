using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculos
{
    public class Matematica
    {
        public int num1;
        public int num2;
        public int result;
        public double resultDiv;
        public String Teste;
        
        public int Soma()
        {
            result = num1 + num2;
            return result;
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

        public int Sub()
        {
            result = num1 - num2;
            return result;
        }

        public int Mult()
        {
            result = num1 * num2;
            return result;
        }

        public double Div()
        {
            if(num1 == 0 || num2 == 0)
            {
                return 0;
            }
            else
            {
                resultDiv = (double)num1 / (double)num2;
                return resultDiv;
            }
        }





    }
}
