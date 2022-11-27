using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modificadores
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Produtos p = new Produtos(); 
            //p.nome = "José";    //tem acesso apenas a propriedade nome, e não ao preço

            int idade;
            double peso;
            double peso2;

            Animal bicho = new Animal();
            idade = bicho.GetIdade();
            peso = bicho.ObeterPeso;
            peso2 = bicho.peso2;
            

            Console.WriteLine("A idade é {0}", idade);
            Console.WriteLine("O peso é {0:F}", peso); //Conseguiu acessar 'peso' que é privada por padrão, utilizando a variavel publica ObterPeso, com o get que retorna o valor de 'peso'
            Console.WriteLine("O peso é {0:F}", peso2); //Conseguiu acessar 'peso2' porque é publica

            Console.ReadLine();
        }
    }
}
