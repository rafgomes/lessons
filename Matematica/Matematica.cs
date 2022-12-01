using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matematica
{
    public class Matematica
    {
        public int Soma(int snum1, int snum2)
        {
            return snum1 + snum2;
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
