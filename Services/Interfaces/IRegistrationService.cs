using EventRegistrationAPI.DTOs.RegistrationDTOs;
using System;
namespace EventRegistrationAPI.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task<RegistrationResponseDTO> RegisterAsync(CreateRegistrationDTO dto);
        Task<RegistrationResponseDTO> CancelRegistrationAsync(int registrationId);
    }
}