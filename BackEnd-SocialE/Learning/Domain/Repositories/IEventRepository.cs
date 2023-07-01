using BackEnd_SocialE.Learning.Domain.Models;
using BackEnd_SocialE.Security.Domain.Models;

namespace BackEnd_SocialE.Learning.Domain.Repositories;

public interface IEventRepository
{
    Task<IEnumerable<Event>> ListAsync();
    Task AddAsync(Event _event);
    Task<Event> FindByIdAsync(int id);
    Task<IEnumerable<Event>> FindByUserIdAsync(int ManagerId);
    void Update(Event _event);
    void Remove(Event _event);
}