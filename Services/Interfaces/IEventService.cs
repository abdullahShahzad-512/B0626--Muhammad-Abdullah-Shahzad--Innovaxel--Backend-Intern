using EventRegistrationAPI.DTOs.EventDTOs;
using System;
namespace EventRegistrationAPI.Services.Interfaces
{
    public interface IEventService
    {
        Task<EventResponseDTO> CreateEventAsync(CreateEventDTO dto);

         Task<IEnumerable<EventResponseDTO>> GetAllEventsAsync(bool upcomingOnly, bool sortByDate);

    }
}