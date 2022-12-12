using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.Clientes
{
    public class Gorilla : IRelatorio
    {
        public Gorilla()
        {
         
        }

        public string Nome { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string GetRelatorio()
        {
            //var result = GetDBRelatio("select nome,idade,telfone, endereco");

            return "camilo, 123,telfone, rua xaraeis";


            
        }
    }
}
