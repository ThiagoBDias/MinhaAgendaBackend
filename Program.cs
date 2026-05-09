using System.Text; 
using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.IdentityModel.Tokens; 
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MinhaAgendaBackend.Data;
using MinhaAgendaBackend.DTOs;
using MinhaAgendaBackend.Repositories;
using MinhaAgendaBackend.Services;
using MinhaAgendaBackend.Validators;

var builder = WebApplication.CreateBuilder(args);

// -- INJEÇÃO E CONFIGURAÇÃO DO JWT --
builder.Services.AddScoped<ITokenService, TokenService>();
var secret = builder.Configuration["JwtConfig:Secret"] 
    ?? throw new InvalidOperationException("Secret não encontrado");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
            ValidAudience = builder.Configuration["JwtConfig:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
        };
    });

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

// 3. Injeção de Dependência da sua regra de negócio
builder.Services.AddScoped<IAtividadeRepository, AtividadeRepository>();
builder.Services.AddScoped<IAtividadeService, AtividadeService>();
builder.Services.AddScoped<IValidator<CreateAtividadeRequest>, CreateAtividadeValidator>();

var app = builder.Build();

// -- PIPELINE DE EXECUÇÃO (A ORDEM AQUI É CRÍTICA) --
app.UseCors("PermitirTudo");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection(); 

// 4. LIGANDO O ESCUDO DE SEGURANÇA
app.UseAuthentication(); // 1º Lê o Token JWT (Quem é você?)
app.UseAuthorization();  // 2º Verifica se tem permissão (Pode passar?)

app.MapControllers();

app.Run();