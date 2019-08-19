using LocadoraAcmeApp.AcessoDados.Interfaces;
using LocadoraAcmeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraAcmeApp.Controllers
{
    [Authorize]
    public class CarrosController : Controller
    {
        private readonly ICarroRepositorio _carroRepositorio;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<CarrosController> _logger;

        public CarrosController(ICarroRepositorio carroRepositorio, IHostingEnvironment hostingEnvironment, ILogger<CarrosController> logger)
        {
            _carroRepositorio = carroRepositorio;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Listando todos os carros");
            return View(await _carroRepositorio.PegarTodos().ToListAsync());
        }


        public IActionResult Create()
        {
            _logger.LogInformation("Novo carro");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarroId,Nome,Marca,Foto,PrecoDiaria")] Carro carro, IFormFile arquivo)
        {
            if (ModelState.IsValid)
            {
                if (arquivo != null)
                {
                    _logger.LogInformation("Criando link da pasta");
                    var linkUpload = Path.Combine(_hostingEnvironment.WebRootPath, "Imagens");

                    using (FileStream fileStream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create))
                    {
                        _logger.LogInformation("Copiando arquivo para pasta");
                        await arquivo.CopyToAsync(fileStream);
                        _logger.LogInformation("Arquivo copiado");
                        carro.Foto = "~/Imagens/" + arquivo.FileName;
                    }
                }
                _logger.LogInformation("Salvando novo carro");

                await _carroRepositorio.Inserir(carro);
                return RedirectToAction(nameof(Index));
            }
            _logger.LogError("Informações inválidas");
            return View(carro);
        }

        public async Task<IActionResult> Edit(int id)
        {

            _logger.LogInformation("Atualizar carro");

            var carro = await _carroRepositorio.PegarPeloId(id);
            if (carro == null)
            {
                _logger.LogError("Carro não encontrado");
                return NotFound();
            }

            TempData["FotoCarro"] = carro.Foto;

            return View(carro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarroId,Nome,Marca,Foto,PrecoDiaria")] Carro carro, IFormFile arquivo)
        {
            if (id != carro.CarroId)
            {
                _logger.LogError("Carro não encontrado");
                return NotFound();
            }
            carro.Foto = TempData["FotoCarro"].ToString();
            if (ModelState.IsValid)
            {
                if (arquivo != null)
                {
                    _logger.LogInformation("Criando link da pasta");
                    var linkUpload = Path.Combine(_hostingEnvironment.WebRootPath, "Imagens");

                    using (FileStream fileStream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create))
                    {
                        _logger.LogInformation("Copiando arquivo para pasta");
                        await arquivo.CopyToAsync(fileStream);
                        _logger.LogInformation("Arquivo copiado");
                        carro.Foto = "~/Imagens/" + arquivo.FileName;
                    }
                }

                else carro.Foto = TempData["FotoCarro"].ToString();

                _logger.LogInformation("Atualizando carro");
                await _carroRepositorio.Atualizar(carro);
                return RedirectToAction(nameof(Index));
            }
            return View(carro);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var carro = await _carroRepositorio.PegarPeloId(id);
            string fotoCarro = carro.Foto;
            fotoCarro = fotoCarro.Replace("~", "wwwroot");
            System.IO.File.Delete(fotoCarro);

            _logger.LogInformation("Excluindo carro");
            await _carroRepositorio.Excluir(id);
            return Json("Carro excluído");
        }
    }
}
