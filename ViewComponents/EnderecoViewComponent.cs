﻿using LocadoraAcmeApp.AcessoDados.Interfaces;
using LocadoraAcmeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraAcmeApp.ViewComponents
{
    public class EnderecoViewComponent : ViewComponent
    {
        private readonly Contexto _contexto;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public EnderecoViewComponent(Contexto contexto, IUsuarioRepositorio usuarioRepositorio)
        {
            _contexto = contexto;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var usuario = await _usuarioRepositorio.PegarUsuarioLogado(HttpContext.User);

            return View(await _contexto.Enderecos.Where(e => e.UsuarioId == usuario.Id).ToListAsync());

        }
    }
}
