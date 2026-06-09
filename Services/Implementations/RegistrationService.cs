using EventRegistrationAPI.Data;
using EventRegistrationAPI.DTOs.RegistrationDTOs;
using EventRegistrationAPI.Exceptions;
using EventRegistrationAPI.Repositories.Interfaces;
using EventRegistrationAPI.Services.Interfaces;

namespace EventRegistrationAPI.Services.Implementations
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IEventRepository _eventRepository;
        private readonly AppDbContext _context;

        public RegistrationService(IRegistrationRepository reg,
                                   IEventRepository ev,
                                   AppDbContext context)
        {
            _registrationRepository = reg;
            _eventRepository = ev;
            _context = context;
        }
        public async Task<RegistrationResponseDTO> CancelRegistrationAsync(int registrationId)
        {
            var reg = await _registrationRepository.GetByIdAsync(registrationId);
            if (reg == null)
                throw new NotFoundException("Registration not found");
            if (reg.isCancelled)
                throw new ConflictException("Registration is already Cacncelled");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                reg.isCancelled = true;
                await _registrationRepository.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            return new RegistrationResponseDTO
            {
                RegistrationId = reg.RegistrationId,
                Username = reg.Username,
                EventId = reg.EventId,
                RegisteredAt = reg.RegisteredAt,
                IsCancelled = reg.isCancelled
            };
        }
        public async Task<RegistrationResponseDTO> RegisterAsync(CreateRegistrationDTO dto)
        {
            var ev = await _eventRepository.GetByIdAsync(dto.EventId);
            if (ev == null)
                throw new NotFoundException("Event not found");

            var existing = await _registrationRepository.GetAsync(dto.Username, ev);
            if (existing != null && !existing.isCancelled)
                throw new ConflictException("User already registered");

            var activeRegs = await _registrationRepository.GetAllActiveAsync(dto.EventId);
            if (activeRegs.Count() >= ev.TotalSeats)
                throw new ConflictException("Event is full");
            var reg = new Registration
            {
                EventId = dto.EventId,
                Username = dto.Username,
                Event = ev,
                RegisteredAt = DateTime.Now,
                isCancelled = false

            };
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            { 
                await _registrationRepository.AddAsync(reg);
                await _registrationRepository.SaveChangesAsync();
                await transaction.CommitAsync();

            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            var res=new RegistrationResponseDTO 
            {
                RegistrationId=reg.RegistrationId,
                Username=reg.Username,
                EventId=reg.EventId,
                RegisteredAt=reg.RegisteredAt,
                IsCancelled=reg.isCancelled
            };
            return res;
        }
    }
}
