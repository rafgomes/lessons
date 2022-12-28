using AlunoAPI.Entities;

namespace AlunoAPI.Repository
{
    public class AlunoRepository
    {
        public static List<Aluno> BancoDeDados { get; set; } = new List<Aluno>();

        public void Carga()
        {
            Aluno aluno1 = new Aluno(1, "Rafael", "Matriculado", 11111);
            Aluno aluno2 = new Aluno(2, "Camilo", "Matriculado", 22222);
            Aluno aluno3 = new Aluno(3, "Jose", "Cancelado", 33333);
            Aluno aluno4 = new Aluno(4, "Maria", "Trancado", 44444);

            BancoDeDados.Add(aluno1);
            BancoDeDados.Add(aluno2);
            BancoDeDados.Add(aluno3);
            BancoDeDados.Add(aluno4);
        }
    }
}
