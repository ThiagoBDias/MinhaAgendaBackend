using Microsoft.AspNetCore.Mvc;
using MinhaAgendaBackend.DTOs;
using MinhaAgendaBackend.Services;

namespace MinhaAgendaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtividadesController : ControllerBase
    {
        private readonly IAtividadeService _service;

        // Injeção de Dependência: o .NET entrega o Service pronto para uso aqui
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
        public async Task<IActionResult> CreateAtividade([FromBody] CreateAtividadeRequest request)
        {
            try
            {
                var novaAtividade = await _service.CreateAtividadeAsync(request);
                return StatusCode(201, novaAtividade); // Retorna HTTP 201 (Criado)
            }
            catch (FluentValidation.ValidationException ex)
            {
                // Se a validação falhar, devolve HTTP 400 com os erros exatos
                return BadRequest(new { erros = ex.Errors.Select(e => e.ErrorMessage) });
            }
        }
    }
    

}