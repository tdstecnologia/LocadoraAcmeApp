using LocadoraAcmeApp.AcessoDados.Interfaces;
using LocadoraAcmeApp.Models;
using LocadoraAcmeApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LocadoraAcmeApp.Controllers
{

    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(IUsuarioRepositorio usuarioRepositorio, ILogger<UsuariosController> logger)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _logger = logger;
        }


        public async Task<IActionResult> Registro()
        {
            if (User.Identity.IsAuthenticated)
                await _usuarioRepositorio.EfetuarLogOut();

            _logger.LogInformation("Entrando na página de registro");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegistroViewModel registro)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario
                {
                    UserName = registro.NomeUsuario,
                    Email = registro.Email,
                    CPF = registro.CPF,
                    Telefone = registro.Telefone,
                    Nome = registro.Nome,
                    PasswordHash = registro.Senha
                };

                _logger.LogInformation("Tentando criar um usuário");
                var resultado = await _usuarioRepositorio.SalvarUsuario(usuario, registro.Senha);

                if (resultado.Succeeded)
                {
                    _logger.LogInformation("Novo usuário criado");
                    _logger.LogInformation("Atribuindo nível de acesso ao novo usuário");
                    var nivelAcesso = "Administrador";

                    await _usuarioRepositorio.AtribuirNivelAcesso(usuario, nivelAcesso);
                    _logger.LogInformation("Atribuição concluída");

                    _logger.LogInformation("Logando o usuário");
                    await _usuarioRepositorio.EfetuarLogin(usuario, false);
                    _logger.LogInformation("Usuário logado com sucesso");

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _logger.LogError("Erro ao criar o usuário");

                    foreach (var erro in resultado.Errors)
                        ModelState.AddModelError("", erro.Description.ToString());
                }

            }
            _logger.LogError("Informações de usuário inválidas");
            return View(registro);
        }

    }
}
