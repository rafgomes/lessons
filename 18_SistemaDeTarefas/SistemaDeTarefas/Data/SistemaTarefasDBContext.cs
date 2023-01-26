using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data.Map;
using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Data
{
    public class SistemaTarefasDBContext : DbContext
    {
        //Está trabalhando com ORM
        
        public SistemaTarefasDBContext(DbContextOptions<SistemaTarefasDBContext> options)
            :base(options)
        {

        }

        public DbSet<UsuarioModel> Usuarios { get; set; } //representa a tabela Usuarios no banco de dados (vai criar a tabela)
        public DbSet<TarefaModel> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new TarefaMap());
            base.OnModelCreating(modelBuilder); 
        }
    }
}
