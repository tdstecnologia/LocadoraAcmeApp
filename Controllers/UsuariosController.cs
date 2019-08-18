﻿using LocadoraAcmeApp.AcessoDados.Interfaces;
using LocadoraAcmeApp.Models;
using LocadoraAcmeApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Listando informações");
            return View(await _usuarioRepositorio.PegarUsuarioLogado(User));
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

                    return RedirectToAction("Index", "Usuarios");
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

        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Pegando o usuário pelo email");
                var usuario = await _usuarioRepositorio.PegarUsuarioPeloEmail(login.Email);
                PasswordHasher<Usuario> passwordHasher = new PasswordHasher<Usuario>();

                if (usuario != null)
                {
                    _logger.LogInformation("Verificando informações do usuário");
                    if (passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, login.Senha) != PasswordVerificationResult.Failed)
                    {
                        _logger.LogInformation("Informações corretas. Logando o usuário");
                        await _usuarioRepositorio.EfetuarLogin(usuario, false);

                        return RedirectToAction("Index", "Usuarios");
                    }

                    _logger.LogError("Informações inválidas");
                    ModelState.AddModelError("", "Email e/ou senha inválidos");
                }

                _logger.LogError("Informações inválidas");
                ModelState.AddModelError("", "Email e/ou senha inválidos");
            }
            return View(login);
        }

        public async Task<IActionResult> LogOut()
        {
            await _usuarioRepositorio.EfetuarLogOut();

            return RedirectToAction("Login", "Usuarios");
        }


    }
}
