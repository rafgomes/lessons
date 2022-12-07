using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqBasicOperations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Fonte De Dados
            List<Costumers> costumers = new List<Costumers>()
            {
                new Costumers(){firstName = "Alex", lastName = "Barbosa", age = 20, city = "Curitiba"},
                new Costumers(){firstName = "Bruna", lastName = "Oliveira", age = 58, city = "Belo Horizonte"},
                new Costumers(){firstName = "Paulo", lastName = "Rosa", age = 25, city = "Curitiba"},
                new Costumers(){firstName = "Enzo", lastName = "Nutella", age = 13, city = "São Paulo"}
            };
            #endregion

            #region Filtragem
            ////Filtragem e Projeção SELECT - sintaxe de consultas
            var costumerQuery = from costumer in costumers
                                where costumer.age >= 18 && costumer.city == "Curitiba"
                                select new { FullName = $"{costumer.firstName} {costumer.lastName}", costumer.age };


            //Filtragem e Projeção SELECT - sintaxe de Método
            var costumerQueryMethod = costumers
                .Where(costumer => costumer.age >= 18 && costumer.city == "Curitiba")
                .Select(costumer => new { FullName = $"{costumer.firstName} {costumer.lastName}", costumer.age });
            #endregion

            #region Ordenação
            //Ordenação - sintaxe de consulta
            var costumerQueryOrder = from costumer in costumers
                                     where costumer.age >= 18
                                     orderby costumer.firstName descending
                                     select new { FullName = $"{costumer.firstName} {costumer.lastName}", costumer.age };

            //Ordenação - sintaxe de Metodo
            var costumerQueryOrderMethod = costumers
                                        .Where(costumer => costumer.age == 18)
                                        .OrderBy(costumer => costumer.firstName)
                                        .ThenBy(costumer => costumer.lastName)
                                        .Select(costumer => new { FullName = $"{costumer.firstName} {costumer.lastName}", costumer.age });

            #endregion

            #region Agrupamento
            //Agrupamento - Sintaxe De Consulta
            var costumerQueryGroup = from costumer in costumers
                                     group costumer by costumer.city;

            //Agrupamento - Sintaxe De Metodo
            var costumerQueryGroupMethod = costumers.GroupBy(costumer => costumer.city);
            
            #endregion

            #region Consulta
            //foreach (var costumer in costumerQuery)
            //{
            //    Console.WriteLine($"Costumer Name: {costumer.FullName} | Age: {costumer.age}");
            //}

            foreach (var costumerGroup in costumerQueryGroup)
            {
                Console.WriteLine($"Key: {costumerGroup.Key}");
                foreach (var costumer in costumerGroup)
                {
                    Console.WriteLine($"Costumer Name: {costumer.firstName} | Age: {costumer.age}");
                }
            }
            #endregion

            Console.ReadLine();
        }
    }
}
