using AlunoAPI.Entities;
using AlunoAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AlunoAPI.Endpoints
{
    public class AlunosGet
    {
        public static string Template => "/alunos";
        public static string[] Metodo => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Func => Acao;

        public static IResult Acao()
        {
            AlunoRepository alunoCarga = new AlunoRepository();
            AlunoRepository.BancoDeDados = new List<Aluno>();

            alunoCarga.Carga();

            return Results.Ok(AlunoRepository.BancoDeDados);

        }

    }
}
