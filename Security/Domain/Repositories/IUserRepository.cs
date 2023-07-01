using BackEnd_SocialE.Security.Domain.Models;

namespace BackEnd_SocialE.Security.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> ListAsync();
    Task AddAsync(User user);
    Task<User> FindByIdAsync(int id);
    Task<User> FindByEmailAsync(string email);
    public bool ExistsByUsername(string username);
    User FindById(int id);
    void Update(User user);
    void Remove(User user);
}