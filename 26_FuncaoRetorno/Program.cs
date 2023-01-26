using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FuncaoRetorno
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //string resultado = Frase();
            //Console.WriteLine(resultado);
            //Console.ReadKey();

            int a = 10;
            int b = 10;

            Matematica result = new Matematica();
            int resultadoDaSoma = result.Soma(a, b);
     

            Console.WriteLine(resultadoDaSoma);

            int resultadoDaSoma2 = result.Soma(a, b);
            Console.WriteLine(resultadoDaSoma2);


            //Console.WriteLine(resultadoDaSoma);

            string resultadoTaskString = await result.Frase();

            Task<int> resultadoDaSomaAsync = result.SomaAsync(a, b);

            Task<int> resultadoDaSomaAsync2 = result.SomaAsync(a, b);

            var resultadoWhenAll = await Task.WhenAll(resultadoDaSomaAsync, resultadoDaSomaAsync2);
            Console.WriteLine(resultadoWhenAll[0]);
            Console.WriteLine(resultadoWhenAll[1]);
            Console.ReadKey();

            List<Task<int>> tasks = new List<Task<int>>();
            tasks.Add(result.SomaAsync(1, 2));
            tasks.Add(result.SomaAsync(1, 3));
            tasks.Add(result.SomaAsync(1, 4));
            tasks.Add(result.SomaAsync(1, 5));
            for (int i = 0; i<10; i++)
            {
                tasks.Add(result.SomaAsync(i, 1));

            }
            await Task.WhenAll(tasks);

            Task<Dictionary<string, List<string>>> taskDictionaryTeste = Task.FromResult(new Dictionary<string, List<string>>());
            //Dictionary<string, List<string>> dicionario = await taskDictionaryTeste;
            Dictionary<string, List<string>> dicionario = taskDictionaryTeste.Result ;
            foreach(KeyValuePair<string, List<string>> item in dicionario)
            {
                Console.WriteLine($"Alunos da escola: {item.Key}");
                foreach(string aluno in item.Value)
                {
                    Console.WriteLine($"Aluno: {aluno}");
                }
            }

            foreach (string key in dicionario.Keys)
            {
                Console.WriteLine($"Alunos da escola: {key}");
                foreach (string aluno in dicionario[key])
                {
                    Console.WriteLine($"Aluno: {aluno}");
                }
            }




        }

        //static string Frase()
        //{
        //    return "Retorno da função string";
        //}


        //                     SINTAXE
        //Local*     Tipo*    Retorno       Nome (Parametros)
        //public    Static     Void     Executar(string teste);
        // * Local e tipo não são obrigatorios serem declarados, quando nao tem Local ela automaticamente é PUBLIC

    }

    public class Matematica
    {
        public int Soma(int a, int b)
        {
            
            int c = a + b;
            Thread.Sleep(5000);
            return c;
            
        }

        public async Task<int> SomaAsync(int a, int b)
        {
            int c = a + b;
            await Task.Delay(TimeSpan.FromSeconds(5));
            //return Task.FromResult<int>(c); //converte int em Task<int>
            return c; 
        }

        public async Task<int> SomaAsync2(int a, int b)
        {
            int c = a + b;
            await Task.Delay(0);
            return c;
        }

        public Task<string> Frase()
        {
            return Task.FromResult<string>("Hello World");
        }

        

    }
}
