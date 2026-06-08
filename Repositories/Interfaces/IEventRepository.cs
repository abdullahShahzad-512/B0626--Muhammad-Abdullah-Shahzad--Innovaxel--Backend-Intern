using System;
namespace EventRegistrationAPI.Repositories.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(int id);
        Task AddAsync(Event ev);
        Task<bool> ExistsAsync(string name);
        Task SaveChangesAsync();
    }
}