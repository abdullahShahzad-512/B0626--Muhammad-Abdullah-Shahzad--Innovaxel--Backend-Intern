using System;
namespace EventRegistrationAPI.Repositories.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<Registration?> GetAsync(string username,Event ev);
        Task AddAsync(Registration reg);
        Task<IEnumerable<Registration>> GetAllActiveAsync(int eventId);
        Task<Registration?> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}