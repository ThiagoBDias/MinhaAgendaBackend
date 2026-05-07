using Microsoft.EntityFrameworkCore;
using MinhaAgendaBackend.Data; // Ajuste para o namespace correto do seu AppDbContext
using MinhaAgendaBackend.Models;

namespace MinhaAgendaBackend.Repositories
{
    // Interface para garantir a inversão de controle
    public interface IAtividadeRepository
    {
        Task<IEnumerable<Atividade>> GetAllAsync();
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
            // Vai no SQLite e busca todas as atividades de forma assíncrona
            return await _context.Atividades.ToListAsync();
        }
    }
}