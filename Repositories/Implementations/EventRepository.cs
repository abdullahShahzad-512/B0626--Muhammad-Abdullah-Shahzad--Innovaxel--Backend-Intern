using EventRegistrationAPI.Data;
using EventRegistrationAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;


namespace EventRegistrationAPI.Repositories.Implementations
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Event ev)
        {
            await _context.Events.AddAsync(ev);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.Events.AnyAsync(e => e.Name == name);
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}