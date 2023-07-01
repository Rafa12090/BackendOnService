using BackEnd_SocialE.Learning.Domain.Models;
using BackEnd_SocialE.Learning.Domain.Services.Communication;
using BackEnd_SocialE.Security.Domain.Models;

namespace BackEnd_SocialE.Learning.Domain.Services;

public interface IEventService
{
    Task<IEnumerable<Event>> ListAsync();
    Task<IEnumerable<Event>> ListByUserIdAsync(int ManagerId);
    Task<EventResponse> SaveAsync(Event _event);
    Task<EventResponse> UpdateAsync(int id, Event _event);
    Task<EventResponse> DeleteAsync(int id);
}