using EventRegistrationAPI.Data;
using EventRegistrationAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class RegistrationRepository : IRegistrationRepository
{
    private readonly AppDbContext _context;
    public RegistrationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Registration reg)
    {
        await _context.Registrations.AddAsync(reg);
    }

    public async Task<IEnumerable<Registration>> GetAllActiveAsync(int evId)
    {
        return await _context.Registrations.Where(r => r.EventId == evId && r.isCancelled == false).ToListAsync();

    }

    public async Task<Registration?> GetAsync(string username, Event ev)
    {
        return await _context.Registrations
            .FirstOrDefaultAsync(r => r.Username == username && r.EventId == ev.EventId);
    }

    public async Task<Registration?> GetByIdAsync(int id)
    {
        return await _context.Registrations.FindAsync(id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}