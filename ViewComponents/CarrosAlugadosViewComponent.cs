using LocadoraAcmeApp.AcessoDados.Interfaces;
using LocadoraAcmeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraAcmeApp.ViewComponents
{
    public class CarrosAlugadosViewComponent : ViewComponent
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly Contexto _contexto;

        public CarrosAlugadosViewComponent(IUsuarioRepositorio usuarioRepositorio, Contexto contexto)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var usuarioLogado = await _usuarioRepositorio.PegarUsuarioLogado(HttpContext.User);

            return View(await _contexto.Alugueis.Include(a => a.Carro).Where(a => a.UsuarioId == usuarioLogado.Id).ToListAsync());
        }
    }
}
