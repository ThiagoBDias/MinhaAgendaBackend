using Microsoft.EntityFrameworkCore;
using MinhaAgendaBackend.Models;

namespace MinhaAgendaBackend.Data
{
    // O DbContext é a classe que gerencia a conexão com o Banco de Dados
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Aqui dizemos que queremos uma tabela chamada "Atividades" baseada no nosso modelo
        public DbSet<Atividade> Atividades { get; set; }
    }
}