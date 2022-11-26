using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulaEscopo //escopo do namespace
{
    internal class Program
    {

        static int num10 = 10; //variavel pertecente a classe, é global, pode ser acessada por todos os escopos
        
        static void Main(string[] args)   //     Método MAIN
        {                                 //   Unica área possivel de acesso a variavel 'num' (é uma variável local)
            int num = 0;                  //   ela pertence apenas ao método MAIN   
                                          //  
            Console.WriteLine(num10);     // 
        }                                 //


        void teste()
        {
            int num = 0;                // A variavel pode ter o mesmo nome da variavel local de outro método
            Console.WriteLine(num);     // ela não pode ser acessada, pois num10 é uma variavel static, e o metodo tambem precisa ser static
        }

        void teste2()
        {
            int num = 50;
                if(num == 50)
                {                                   //IF é um escopo
                string texto = "O numero é:";
                Console.WriteLine($"{texto} 50!");
                } else
                {
                Console.WriteLine("O numero não é 50!"); //não é possivel acessar a variavel 'texto', pois ELSE é outro escopo
                }

            //aqui dentro do metodo 'teste2' tambem não é possivel acessar a variavel 'texto'
        }


      
    }                                     

}
