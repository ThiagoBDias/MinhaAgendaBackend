using MinhaAgendaBackend.DTOs;
using MinhaAgendaBackend.Repositories;

namespace MinhaAgendaBackend.Services
{
    public interface IAtividadeService
    {
        Task<IEnumerable<AtividadeResponse>> GetAtividadesAsync();
    }

    public class AtividadeService : IAtividadeService
    {
        private readonly IAtividadeRepository _repository;

        public AtividadeService(IAtividadeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AtividadeResponse>> GetAtividadesAsync()
        {
            var atividadesDb = await _repository.GetAllAsync();

            // Transforma a Entidade do Banco no DTO de Resposta
            return atividadesDb.Select(a => new AtividadeResponse(
                a.Id,
                a.Name,
                a.Start,
                a.End,
                a.Cat
            ));
        }
    }
}