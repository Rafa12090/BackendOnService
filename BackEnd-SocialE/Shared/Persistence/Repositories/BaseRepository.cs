using BackEnd_SocialE.Shared.Persistence.Contexts;

namespace BackEnd_SocialE.Shared.Persistence.Repositories;

public class BaseRepository {
    protected readonly AppDbContext _context;

    public BaseRepository(AppDbContext context) {
        _context = context;
    }
}