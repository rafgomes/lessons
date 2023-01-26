using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntendendoExceptions
{
    internal class NumeroNegativoException : Exception
    {
        public NumeroNegativoException()
        {

        }

        public NumeroNegativoException(string message) : base(message)
        {

        }

    }
}
