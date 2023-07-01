using BackEnd_SocialE.Learning.Domain.Models;
using BackEnd_SocialE.Learning.Domain.Repositories;
using BackEnd_SocialE.Shared.Persistence.Contexts;
using BackEnd_SocialE.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_SocialE.Learning.Persistence.Repositories;

public class PaymentRepository: BaseRepository, IPaymentRepository
{
    public PaymentRepository(AppDbContext context) : base(context) { }
    public async Task<IEnumerable<Payment>> ListAsync() {
        return await _context.Payments
            .Include(p => p.Event)
            .Include(p => p.User)
            .ToListAsync();
    }

    public async Task AddAsync(Payment payment) {
        await _context.Payments.AddAsync(payment);
    }

    public async Task<Payment> FindByIdAsync(int id) {
        return await _context.Payments
            .Include(p => p.Event)
            .Include(p=>p.User)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Payment>> FindByUserIdAsync(int UserId) {
        return await _context.Payments
            .Where(p => p.UserId == UserId)
            .Include(p => p.User)
            .ToListAsync();
    }

    public async Task<IEnumerable<Payment>> FindByEventIdAsync(int EventId) {
        return await _context.Payments
            .Where(p => p.EventId == EventId)
            .Include(p => p.Event)
            .ToListAsync();
    }

    public void Update(Payment payment) {
        _context.Payments.Update(payment);
    }

    public void Remove(Payment payment)
    {
        _context.Payments.Remove(payment);
    }
}