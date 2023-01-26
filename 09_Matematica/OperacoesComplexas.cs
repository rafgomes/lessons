using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matematica
{
    public class OperacoesComplexas : Matematica
    {
        public double SomaMultiDiv(int soma1, int soma2, int multi, int div)
        {
            int resultSoma = base.Soma(soma1, soma2);
            int resultMulti = base.Mult(resultSoma, multi);
            double result = base.Div(resultMulti, div);

            return Div(Mult(Soma(soma1, soma2), multi), div);
                        
        }

        public int Soma(int a, int b)
        {
            return base.Soma(a, b);
        }
    }
}
