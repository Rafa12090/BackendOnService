using BackEnd_SocialE.Security.Domain.Models;

namespace BackEnd_SocialE.Security.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    public string GenerateToken(User user);
    public int? ValidateToken(string token);
}