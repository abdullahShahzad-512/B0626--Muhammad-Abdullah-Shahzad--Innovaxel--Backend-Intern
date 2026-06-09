using EventRegistrationAPI.DTOs.EventDTOs;
using EventRegistrationAPI.Exceptions;
using EventRegistrationAPI.Repositories.Interfaces;
using EventRegistrationAPI.Services.Interfaces;
using System.Diagnostics.Eventing.Reader;

namespace EventRegistrationAPI.Services.Implementations
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IRegistrationRepository _registrationRepository;


        public EventService(IEventRepository eventRepository, IRegistrationRepository registrationRepository)
        {
            _eventRepository = eventRepository;
            _registrationRepository = registrationRepository;
        }
        public async Task<EventResponseDTO> CreateEventAsync(CreateEventDTO dto)
        {
           bool isExist= await _eventRepository.ExistsAsync(dto.Name);
            if (isExist)
                throw new ConflictException("Name of Event must be unique");
            if (dto.Date <= DateTime.Now)
                throw new Exception("Event must be in future");
            Event ev=new Event { Name=dto.Name ,
                                 Date=dto.Date,
                                 TotalSeats=dto.TotalSeats,
                                  CreatedAt=DateTime.Now};
            await _eventRepository.AddAsync(ev);
            await _eventRepository.SaveChangesAsync();
            EventResponseDTO evResponse = new EventResponseDTO
            {
                EventId=ev.EventId,
                Name = ev.Name, 
                Date = ev.Date,
                TotalSeats = ev.TotalSeats,
                AvailableSeats=ev.TotalSeats,
                TotalRegistrations=0

            };
            return evResponse;

        }

        public async Task<IEnumerable<EventResponseDTO>> GetAllEventsAsync(bool upcomingOnly, bool sortByDate)
        {
            var events=await _eventRepository.GetAllAsync();
            if (upcomingOnly && sortByDate)
            {
                events = events.Where(e => e.Date > DateTime.Now).OrderBy(e => e.Date);
            }
            else if(upcomingOnly)
            {
                events = events.Where(e => e.Date > DateTime.Now); 
            }
            else
            {
                events = events.OrderBy(e => e.Date);
            }
            var EventResponses=new List<EventResponseDTO>();
            foreach(var ev in events)
            {
                var activeRegistrations = await _registrationRepository.GetAllActiveAsync(ev.EventId);
                int totalReg = activeRegistrations.Count();
                int availableSeats = ev.TotalSeats - totalReg;
                EventResponses.Add(new EventResponseDTO
                {
                    EventId = ev.EventId,
                    Name = ev.Name,
                    Date = ev.Date,
                    TotalSeats = ev.TotalSeats,
                    AvailableSeats = availableSeats,
                    TotalRegistrations = totalReg
                });
            }
            return EventResponses; 
        }
    }
}
