﻿using LocadoraAcmeApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LocadoraAcmeApp.AcessoDados.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorioGenerico<Usuario>
    {
        Task<Usuario> PegarUsuarioLogado(ClaimsPrincipal usuario);

        Task<IdentityResult> SalvarUsuario(Usuario usuario, string senha);

        Task AtualizarUsuario(Usuario usuario);

        Task AtribuirNivelAcesso(Usuario usuario, string nivelAcesso);

        Task EfetuarLogin(Usuario usuario, bool lembrar);

        Task EfetuarLogOut();

        Task<Usuario> PegarUsuarioPeloEmail(string email);
    }
}
