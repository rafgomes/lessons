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
        
        public int Soma()
        {
            result = num1 + num2;
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
