using BackEnd_SocialE.Learning.Domain.Models;
using BackEnd_SocialE.Learning.Domain.Repositories;
using BackEnd_SocialE.Security.Domain.Models;
using BackEnd_SocialE.Shared.Persistence.Contexts;
using BackEnd_SocialE.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_SocialE.Learning.Persistence.Repositories;

public class EventRepository : BaseRepository, IEventRepository
{
    public EventRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Event>> ListAsync() {
        return await _context.Events
            .Include(p => p.Manager)
            .ToListAsync();
    }

    public async Task AddAsync(Event @event) {
        await _context.Events.AddAsync(@event);
    }

    public async Task<Event> FindByIdAsync(int id) {
        return await _context.Events
            .Include(p => p.Manager)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Event>> FindByUserIdAsync(int ManagerId)
    {
        return await _context.Events
            .Where(p => p.ManagerId == ManagerId)
            .Include(p => p.Manager)
            .ToListAsync();
    }

    public void Update(Event _event) {
        _context.Events.Update(_event);
    }

    public void Remove(Event _event) {
        _context.Events.Remove(_event);
    }
}