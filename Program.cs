using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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

// Configura o Banco de Dados SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuração Profissional do Swagger (Prioridade 2)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "T-Control API", 
        Version = "v1",
        Description = "Backend da Agenda Pessoal T-Control"
    });

    // Configura o botão "Authorize" (Cadeado) no Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT. Exemplo: Bearer {seu_token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});

// Configura Política de Acesso (CORS) para o Android/Web consumir a API
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// ============================================================
// 3. INJEÇÃO DE DEPENDÊNCIA (ARQUITETURA EM CAMADAS)
// ============================================================
builder.Services.AddScoped<IAtividadeRepository, AtividadeRepository>();
builder.Services.AddScoped<IAtividadeService, AtividadeService>();
builder.Services.AddScoped<IValidator<CreateAtividadeRequest>, CreateAtividadeValidator>();

var app = builder.Build();

// ============================================================
// 4. PIPELINE DE EXECUÇÃO (A ORDEM AQUI É CRÍTICA)
// ============================================================

// O CORS deve vir sempre antes de quase tudo
app.UseCors("PermitirTudo");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "T-Control API v1");
        c.RoutePrefix = "swagger"; // Aceda em http://localhost:5141/swagger
    });
}

// Comente esta linha se for testar a API noutros dispositivos via IP local
// app.UseHttpsRedirection(); 

// Ativação do Escudo de Segurança
app.UseAuthentication(); // 1º - Verifica o Token (Quem é?)
app.UseAuthorization();  // 2º - Verifica a Permissão (O que pode fazer?)

app.MapControllers();

app.Run();