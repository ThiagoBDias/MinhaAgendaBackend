using Microsoft.AspNetCore.Mvc;
using MinhaAgendaBackend.DTOs;
using MinhaAgendaBackend.Services;

namespace MinhaAgendaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Validação direta para testar a arquitetura JWT.
            if (request.Username == "thiago" && request.Password == "123456")
            {
                var token = _tokenService.GenerateToken(request.Username);
                return Ok(new LoginResponse(token));
            }

            // HTTP 401: Acesso Negado
            return Unauthorized(new { erro = "Usuário ou senha inválidos." }); 
        }
    }
}