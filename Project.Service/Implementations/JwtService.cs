using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Project.Service.Interface;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Project.Service.Implementations;

public class JwtService : IJwtService
{
    private readonly string _key;
    private readonly string _issuer;
    private readonly string _audience;

    public JwtService(IConfiguration configuration)
    {
        _key = configuration["Jwt:Key"];
        _issuer = configuration["Jwt:Issuer"];
        _audience = configuration["Jwt:Audience"];
    }
    public string GenerateJwtToken(string email, string role,bool RememberMe, int id)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.NameIdentifier,id.ToString())
                }),
            Expires = RememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddDays(1),
            Issuer = _issuer,
            Audience = _audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public (string, string) ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token))
            return (null, null);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_key);

        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            var role = principal.FindFirst(ClaimTypes.Role)?.Value.Trim();
            var email = principal.FindFirst(ClaimTypes.Email)?.Value.Trim();
            return (role, email);
        }
        catch
        {
            return (null, null);
        }
    }
}
