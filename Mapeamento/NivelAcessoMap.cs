using LocadoraAcmeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraAcmeApp.Mapeamento
{
    public class NivelAcessoMap : IEntityTypeConfiguration<NiveisAcesso>
    {
        public void Configure(EntityTypeBuilder<NiveisAcesso> builder)
        {
            builder.Property(n => n.Descricao).IsRequired().HasMaxLength(400);

            builder.ToTable("tb02_nivel_acesso");
        }
    }
}
