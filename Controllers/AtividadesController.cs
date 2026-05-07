using Microsoft.AspNetCore.Mvc;
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
    }
}