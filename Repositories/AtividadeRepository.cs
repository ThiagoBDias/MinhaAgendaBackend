using Microsoft.EntityFrameworkCore;
using MinhaAgendaBackend.Data;
using MinhaAgendaBackend.Models;

namespace MinhaAgendaBackend.Repositories
{
    public interface IAtividadeRepository
    {
        Task<IEnumerable<Atividade>> GetAllAsync();
        Task<Atividade> AddAsync(Atividade atividade); // Nova linha
    }

    public class AtividadeRepository : IAtividadeRepository
    {
        private readonly AppDbContext _context;

        public AtividadeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Atividade>> GetAllAsync()
        {
            return await _context.Atividades.ToListAsync();
        }

        // Novo método para salvar no banco SQLite
        public async Task<Atividade> AddAsync(Atividade atividade)
        {
            await _context.Atividades.AddAsync(atividade);
            await _context.SaveChangesAsync();
            return atividade;
        }
    }
}