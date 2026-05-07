using FluentValidation;
using MinhaAgendaBackend.DTOs;
using MinhaAgendaBackend.Models;
using MinhaAgendaBackend.Repositories;

namespace MinhaAgendaBackend.Services
{
    public interface IAtividadeService
    {
        Task<IEnumerable<AtividadeResponse>> GetAtividadesAsync();
        Task<AtividadeResponse> CreateAtividadeAsync(CreateAtividadeRequest request); // Nova linha
    }

    public class AtividadeService : IAtividadeService
    {
        private readonly IAtividadeRepository _repository;
        private readonly IValidator<CreateAtividadeRequest> _validator; // Injetando o validador

        public AtividadeService(IAtividadeRepository repository, IValidator<CreateAtividadeRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IEnumerable<AtividadeResponse>> GetAtividadesAsync()
        {
            var atividadesDb = await _repository.GetAllAsync();
            return atividadesDb.Select(a => new AtividadeResponse(a.Id, a.Name, a.Start, a.End, a.Cat));
        }

        public async Task<AtividadeResponse> CreateAtividadeAsync(CreateAtividadeRequest request)
        {
            // 1. Validação (Se falhar, estoura erro e nem chega no banco)
            await _validator.ValidateAndThrowAsync(request);

            // 2. Converte DTO para Model do Banco
            var novaAtividade = new Atividade
            {
                Name = request.Name,
                Start = request.Start,
                End = request.End,
                Cat = request.Cat
            };

            // 3. Salva no banco
            var atividadeSalva = await _repository.AddAsync(novaAtividade);

            // 4. Retorna a resposta limpa
            return new AtividadeResponse(
                atividadeSalva.Id, 
                atividadeSalva.Name, 
                atividadeSalva.Start, 
                atividadeSalva.End, 
                atividadeSalva.Cat
            );
        }
    }
}