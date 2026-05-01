using Microsoft.AspNetCore.Mvc;
using MinhaAgendaBackend.Models;

namespace MinhaAgendaBackend.Controllers
{
    // Isso diz que essa classe é um "Garçom" da API
    [ApiController]
    // Isso define a URL (o endereço) que vamos chamar no navegador
    [Route("api/[controller]")] 
    public class AtividadesController : ControllerBase
    {
        // Esse [HttpGet] diz que quando alguém "pedir" (GET) dados nessa URL, essa função vai rodar
        [HttpGet]
        public IActionResult GetAtividades()
        {
            // Por enquanto, estamos simulando um banco de dados com uma lista fixa
            var minhaLista = new List<Atividade>
            {
                new Atividade { Id = 1, Name = "Treino de luta", Start = "08:00", End = "09:30", Cat = "saude" },
                new Atividade { Id = 2, Name = "Academia", Start = "18:00", End = "19:30", Cat = "saude" },
                new Atividade { Id = 3, Name = "Jogar", Start = "21:00", End = "23:00", Cat = "lazer" }
            };

            // O 'Ok' é a resposta HTTP 200 (Sucesso), que devolve a lista no formato JSON
            return Ok(minhaLista);
        }
    }
}