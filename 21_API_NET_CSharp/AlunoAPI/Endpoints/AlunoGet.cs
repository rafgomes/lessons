﻿using AlunoAPI.Entities;
using AlunoAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AlunoAPI.Endpoints
{
    public class AlunoGet
    {
        public static string Template => "/aluno/{Id:int}";
        public static string[] Metodo => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Func => Acao;

        public static IResult Acao([FromRoute] int id)
        {
            AlunoRepository alunoCarga = new AlunoRepository();
            alunoCarga.Carga();

            Aluno alunoEncontrado = AlunoRepository.BancoDeDados.FirstOrDefault(a => a.Id.Equals(id));

            if(alunoEncontrado != null)
            {
                return Results.Ok(alunoEncontrado);
            }
            else
            {
                return Results.NotFound();
            }
        }
        

        
    }
}
