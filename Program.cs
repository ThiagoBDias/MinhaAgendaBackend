using Microsoft.EntityFrameworkCore;
using MinhaAgendaBackend.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Configura o Banco de Dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// 2. Configura a política de acesso (CORS)
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// 3. Ativa o CORS ANTES de mapear os controladores
app.UseCors("PermitirTudo");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// COMENTE esta linha abaixo para facilitar o acesso pelo celular via IP
// app.UseHttpsRedirection(); 

app.UseAuthorization();
app.MapControllers();

app.Run();