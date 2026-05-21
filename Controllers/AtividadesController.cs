using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaAgendaBackend.DTOs;
using MinhaAgendaBackend.Services;

namespace MinhaAgendaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // O Escudo do JWT
    public class AtividadesController : ControllerBase
    {
        private readonly IAtividadeService _service;

        public AtividadesController(IAtividadeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAtividades()
        {
            var atividades = await _service.GetAtividadesAsync();
            return Ok(atividades);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AtividadeResponse), 201)] // Sucesso
        [ProducesResponseType(400)] // Erro de validação
        [ProducesResponseType(401)] // Sem Token
        public async Task<IActionResult> CreateAtividade([FromBody] CreateAtividadeRequest request)
        {
            try
            {
                var novaAtividade = await _service.CreateAtividadeAsync(request);
                return StatusCode(201, novaAtividade); // HTTP 201 (Criado)
            }
            catch (FluentValidation.ValidationException ex)
            {
                // Se a validação falhar, devolve HTTP 400 com os erros exatos
                return BadRequest(new { erros = ex.Errors.Select(e => e.ErrorMessage) });
            }
        }

        [HttpPatch("{id}/toggle-status")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var sucesso = await _service.ToggleConclusaoAsync(id);
            
            if (!sucesso)
                return NotFound(new { mensagem = "Atividade não encontrada." });

            return NoContent(); // HTTP 204: Sucesso, sem corpo de resposta
        }
    }
}