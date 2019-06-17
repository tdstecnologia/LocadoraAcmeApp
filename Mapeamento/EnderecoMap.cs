using LocadoraAcmeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraAcmeApp.Mapeamento
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(e => e.EnderecoId);

            builder.Property(e => e.Numero).IsRequired();
            builder.Property(e => e.Rua).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Bairro).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Cidade).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Estado).IsRequired().HasMaxLength(100);


            builder.HasOne(c => c.Usuario).WithMany(c => c.Enderecos).HasForeignKey(c => c.UsuarioId);

            builder.ToTable("tb04_endereco");
        }
    }
}
