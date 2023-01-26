using AlunoAPI.Entities;
using AlunoAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AlunoAPI.Endpoints
{
    public class IndexGet
    {
        public static string Template => "/";
        public static string[] Metodo => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Func => Acao;

        public static IResult Acao()
        {
            return Results.Ok("Hello!!!");
        }
        

        
    }
}