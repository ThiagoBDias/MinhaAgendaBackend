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

// ============================================================
// 1. CONFIGURAÇÃO DE SEGURANÇA (JWT)
// ============================================================
builder.Services.AddScoped<ITokenService, TokenService>();

var secret = builder.Configuration["JwtConfig:Secret"] 
    ?? throw new InvalidOperationException("Secret do JWT não configurado no appsettings.json");

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

// ============================================================
// 2. SERVIÇOS E INFRAESTRUTURA
// ============================================================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger Padrão (Sem o cadeado visual problemático do .NET 10)
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// ============================================================
// 3. INJEÇÃO DE DEPENDÊNCIA
// ============================================================
builder.Services.AddScoped<IAtividadeRepository, AtividadeRepository>();
builder.Services.AddScoped<IAtividadeService, AtividadeService>();
builder.Services.AddScoped<IValidator<CreateAtividadeRequest>, CreateAtividadeValidator>();

var app = builder.Build();

// ============================================================
// 4. PIPELINE DE EXECUÇÃO
// ============================================================
app.UseCors("PermitirTudo");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); 
app.UseAuthorization();  

app.MapControllers();

app.Run();