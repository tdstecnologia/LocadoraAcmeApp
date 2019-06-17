using LocadoraAcmeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraAcmeApp.Mapeamento
{
    public class AluguelMap : IEntityTypeConfiguration<Aluguel>
    {
        public void Configure(EntityTypeBuilder<Aluguel> builder)
        {
            builder.HasKey(a => a.AluguelId);

            builder.Property(a => a.Inicio).IsRequired();
            builder.Property(a => a.Inicio).IsRequired();
            builder.Property(a => a.PrecoTotal).IsRequired();

            builder.HasOne(x => x.Usuario).WithMany(x => x.Alugueis).HasForeignKey(x => x.UsuarioId);
            builder.HasOne(x => x.Carro).WithMany(x => x.Alugueis).HasForeignKey(x => x.CarroId);

            builder.ToTable("tb06_aluguel");
        }
    }
}
