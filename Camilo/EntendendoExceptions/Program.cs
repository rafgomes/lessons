using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntendendoExceptions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = 10;
            int b = 2; //altera os valores de "b" para testar as exceções
            Object objeto = null;



            try //toda instrução que for critica e que lançar uma exceção, deve ficar dentro do try
            {
                if (b < 0 || a < 0)
                {
                    throw new NumeroNegativoException("Exceção por numero negativo!");
                }
                
                int c = a / b; //conjunto de instrução critica (divisão por zero)
                objeto.ToString(); //imposivel converter objeto nulo em string
            }
            catch (DivideByZeroException) //será a exceção que será lançada  //EXCEPTION é a classe pai de todas as exceções
            {
                Console.WriteLine("Exceção lançada!");
            }
            catch (NullReferenceException) //tratando as exceções por nulo
            {
                Console.WriteLine("Exceção por nulo!");
            }
            //catch (Exception) //tratando todas as exceções
            //{
            //    Console.WriteLine("Exceção Geral!");
            //}
            catch (NumeroNegativoException ex) //quando tem uma mensagem ou quero extrair atributos/informações dessa exceção, posso utilizar um objeto que vai retornar esse resultado "ex" (pode ser qualquer nome)
            {
                Console.WriteLine(ex.Message); //vai exibir a mensagem que foi passada quando foi lançada a exceção (lá no throw)
                Console.WriteLine(ex.StackTrace); //mostra o caminho onde foi lançada a exceção
            }
            finally //mesmo tendo todas as exceções tratadas ele cairá aqui
            {
                Console.WriteLine("Finally!");
            }


            Console.ReadKey();
        }
    }
}
