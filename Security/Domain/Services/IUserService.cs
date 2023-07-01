using BackEnd_SocialE.Security.Domain.Models;
using BackEnd_SocialE.Security.Domain.Services.Communication;

namespace BackEnd_SocialE.Security.Services;

public interface IUserService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    Task<IEnumerable<User>> ListAsync();
    Task<User> GetByIdAsync(int id);
    Task RegisterAsync(RegisterRequest model);
    Task UpdateAsync(int id, UpdateRequest model);
    Task DeleteAsync(int id);
}