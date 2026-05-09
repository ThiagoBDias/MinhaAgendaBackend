using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MinhaAgendaBackend.Services
{
    public interface ITokenService
    {
        string GenerateToken(string username);
    }

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string username)
        {
            // 1. Puxa os dados do appsettings.json
            var secret = _configuration["JwtConfig:Secret"] ?? throw new InvalidOperationException("JWT Secret não configurado.");
            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];

            // 2. Transforma a chave em bytes criptográficos
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3. Define as "Claims" (informações que vão dentro do token)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // ID único do token
            };

            // 4. Monta a estrutura do Token (Validade de 8 horas)
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: credentials);

            // 5. Gera a string final para enviar ao Android
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}