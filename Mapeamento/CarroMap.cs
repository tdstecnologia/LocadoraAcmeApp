using LocadoraAcmeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraAcmeApp.Mapeamento
{
    public class CarroMap : IEntityTypeConfiguration<Carro>
    {
        public void Configure(EntityTypeBuilder<Carro> builder)
        {
            builder.HasKey(e => e.CarroId);

            builder.Property(c => c.Nome).IsRequired().HasMaxLength(100); ;
            builder.Property(c => c.Marca).IsRequired().HasMaxLength(100);
            builder.Property(c => c.PrecoDiaria).IsRequired();
            builder.Property(c => c.Foto).IsRequired();


            builder.HasMany(c => c.Alugueis).WithOne(c => c.Carro).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("tb05_carro");
        }
    }
}
