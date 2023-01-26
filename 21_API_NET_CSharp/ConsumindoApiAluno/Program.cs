using ConsumindoApiAluno.Entities;
using ConsumindoApiAluno.Entities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumindoApiAluno
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Informe o Id: ");
            int id = int.Parse(Console.ReadLine());

            AlunoServices alunoServices = new AlunoServices();

            Aluno alunoEncontrado = await alunoServices.Integracao(id);

            if (!alunoEncontrado.Verificacao)
            {
                Console.WriteLine("Aluno Encontrado");
                Console.WriteLine($"Nome: {alunoEncontrado.Nome}");
                Console.WriteLine($"Matricula: {alunoEncontrado.Matricula}");
                Console.WriteLine($"Siutação: {alunoEncontrado.Situacao}");
            }
            else
            {
                Console.WriteLine("Aluno não encontrado!");
            }

            Console.ReadLine();
        }
    }
}
