using LocadoraAcmeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraAcmeApp.Mapeamento
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(u => u.Nome).IsRequired().HasMaxLength(100);
            builder.Property(u => u.CPF).IsRequired();
            builder.HasIndex (u => u.CPF).IsUnique();
            builder.Property(u => u.Telefone).IsRequired().HasMaxLength(30);

            builder.Ignore(u => u.EmailConfirmed);
            builder.Ignore(u => u.AccessFailedCount);
            builder.Ignore(u => u.LockoutEnabled);
            builder.Ignore(u => u.LockoutEnd);
            builder.Ignore(u => u.PhoneNumber);
            builder.Ignore(u => u.PhoneNumberConfirmed);
            builder.Ignore(u => u.TwoFactorEnabled);

            builder.HasMany(u => u.Enderecos).WithOne(u => u.Usuario);
            builder.HasMany(u => u.Alugueis).WithOne(u => u.Usuario);
            builder.HasOne(u => u.Conta).WithOne(u => u.Usuario);

            builder.ToTable("tb01_usuario");
        }
    }
}
