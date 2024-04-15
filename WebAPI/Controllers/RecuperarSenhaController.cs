﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Contexts;
using WebAPI.Utils.Mail;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecuperarSenhaController : ControllerBase
    {
        private readonly VitalContext _context;

        private readonly EmailSendService _emailSendService;
        public RecuperarSenhaController(VitalContext context, EmailSendService emailSendService)
        {
            _context = context;
            _emailSendService = emailSendService;
        }

        [HttpPost]
        public async Task<IActionResult> SendRecoveryCodePassword(string email)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

                if (usuario == null)
                {
                    return NotFound("Usuario nao encontrado!"); 
                }

                Random random = new Random();

                int recoveryCode = random.Next(1000, 9999);

                usuario.CodRecupSenha = recoveryCode;

                await _context.SaveChangesAsync();

                await _emailSendService.SendRecoveryPassword(usuario.Email!, recoveryCode);

                return Ok("Codigo de confirmacao enviado com sucesso");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ValidarCodigoRecuperacaoSenha")]

        public async Task<IActionResult> ValidatePasswordRecoveryCode(string email, int codigo)
        {
            try
            {
                var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    return NotFound("Usuario nao encontrado");
                }

                if (user.CodRecupSenha != codigo)
                {
                    return BadRequest("codigo de recuperaca invalido");
                }

                user.CodRecupSenha = null;

                await _context.SaveChangesAsync();

                return Ok("Codigo de recuperacao esta correto");
            }

            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
