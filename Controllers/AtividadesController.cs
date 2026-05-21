using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaAgendaBackend.DTOs;
using MinhaAgendaBackend.Services;

namespace MinhaAgendaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
        [ProducesResponseType(typeof(AtividadeResponse), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> CreateAtividade([FromBody] CreateAtividadeRequest request)
        {
            try
            {
                var novaAtividade = await _service.CreateAtividadeAsync(request);
                return StatusCode(201, novaAtividade);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(new { erros = ex.Errors.Select(e => e.ErrorMessage) });
            }
        }
    }
}