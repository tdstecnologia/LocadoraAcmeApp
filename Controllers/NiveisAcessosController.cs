﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LocadoraAcmeApp.Models;
using LocadoraAcmeApp.AcessoDados.Interfaces;

namespace LocadoraAcmeApp.Controllers
{
    public class NiveisAcessosController : Controller
    {
        private readonly INivelAcessoRepositorio _nivelAcessoRepositorio;

        public NiveisAcessosController(INivelAcessoRepositorio nivelAcessoRepositorio)
        {
            _nivelAcessoRepositorio = nivelAcessoRepositorio;
        }

        // GET: NiveisAcessoes
        public async Task<IActionResult> Index()
        {
            return View(await _nivelAcessoRepositorio.PegarTodos().ToListAsync());
        }

        // GET: NiveisAcessoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var niveisAcesso = await _nivelAcessoRepositorio.PegarPeloId
                .FirstOrDefaultAsync(m => m.Id == id);
            if (niveisAcesso == null)
            {
                return NotFound();
            }

            return View(niveisAcesso);
        }

        // GET: NiveisAcessoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NiveisAcessoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao,Name")] NiveisAcesso niveisAcesso)
        {
            bool nivelExiste = await _nivelAcessoRepositorio.NivelAcessoExiste(niveisAcesso.Name);

            if (nivelExiste)
            {
                niveisAcesso.NormalizedName = niveisAcesso.Name.ToUpper();
                await _nivelAcessoRepositorio.Inserir(niveisAcesso);
                return RedirectToAction("Index", "NiveisACessos");
            }
            return View(niveisAcesso);
        }

        // GET: NiveisAcessoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var niveisAcesso = await _nivelAcessoRepositorio.PegarPeloId(id);
            if (niveisAcesso == null)
            {
                return NotFound();
            }
            return View(niveisAcesso);
        }

        // POST: NiveisAcessoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Descricao,Id,Name,NormalizedName,ConcurrencyStamp")] NiveisAcesso niveisAcesso)
        {
            if (id != niveisAcesso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _nivelAcessoRepositorio.Update(niveisAcesso);
                    await _nivelAcessoRepositorio.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NiveisAcessoExists(niveisAcesso.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(niveisAcesso);
        }

        // GET: NiveisAcessoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var niveisAcesso = await _nivelAcessoRepositorio.NiveisAcessos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (niveisAcesso == null)
            {
                return NotFound();
            }

            return View(niveisAcesso);
        }

        // POST: NiveisAcessoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var niveisAcesso = await _nivelAcessoRepositorio.NiveisAcessos.FindAsync(id);
            _nivelAcessoRepositorio.NiveisAcessos.Remove(niveisAcesso);
            await _nivelAcessoRepositorio.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NiveisAcessoExists(string id)
        {
            return _nivelAcessoRepositorio.NiveisAcessos.Any(e => e.Id == id);
        }
    }
}
