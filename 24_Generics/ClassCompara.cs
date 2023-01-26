using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    //public class ClassCompara<TipoA, TipoB> //Generics pode ser tambem utilizado na definição da classe
    //{
    //    public void Compara(TipoA p1, TipoB p2) 
    //    {
    //        bool resultado;
    //        resultado = p1.Equals(p2);
    //        Console.WriteLine(resultado);
    //    }
    //}

    public class ClassCompara<TipoA, TipoB> where TipoA : class , new() where TipoB: Delegate //pode tambem restringir informando o o que os tipos são
    {
        public void Compara(TipoA p1, TipoB p2)
        {
            bool resultado;
            resultado = p1.Equals(p2);
            Console.WriteLine(resultado);
        }
    }

}
