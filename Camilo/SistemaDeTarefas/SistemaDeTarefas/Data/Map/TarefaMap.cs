using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Data.Map
{
    public class TarefaMap : IEntityTypeConfiguration<TarefaModel>
    {
        public void Configure(EntityTypeBuilder<TarefaModel> builder)
        {
            builder.HasKey(x => x.Id); //indica que id é a chave primaria
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255); //nome é obrigatorio, com no maximo 255 caracteres
            builder.Property(x => x.Descricao).HasMaxLength(1000); //descricao não é obrigatoria
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
