using Microsoft.EntityFrameworkCore;
using MinhaAgendaBackend.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Configura o Banco de Dados ANTES de construir o app
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Adiciona os controladores (os garçons)
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// 3. Configura o que acontece quando o app já está rodando
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();