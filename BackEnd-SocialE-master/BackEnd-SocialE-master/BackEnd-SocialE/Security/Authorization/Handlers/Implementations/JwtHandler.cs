using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackEnd_SocialE.Security.Authorization.Handlers.Interfaces;
using BackEnd_SocialE.Security.Authorization.Settings;
using BackEnd_SocialE.Security.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd_SocialE.Security.Authorization.Handlers.Implementations;

public class JwtHandler: IJwtHandler {
    private readonly AppSettings _appSettings;
    public JwtHandler(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public string GenerateToken(User user) {
        var tokenHandler = new JwtSecurityTokenHandler();
        Console.WriteLine($"token handler: {tokenHandler.TokenType}");
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        Console.WriteLine($"Secret Key: {key}");
        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        Console.WriteLine($"token: {token.Id}, {token.Issuer}, {token.SecurityKey?.ToString()}");
        return tokenHandler.WriteToken(token);
    }

    public int? ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token)) {return null;}
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        try {
            tokenHandler.ValidateToken(token, new
                TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero }
                , out var validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value);
            return userId;
        }
        catch (Exception e) {
            Console.WriteLine(e); return null;
        }
    }
}