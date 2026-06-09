using EventRegistrationAPI.DTOs.RegistrationDTOs;
using EventRegistrationAPI.Exceptions;
using EventRegistrationAPI.Services.Implementations;
using EventRegistrationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationsController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationsController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateRegistrationDTO dto)
        {
            try
            {
                var result = await _registrationService.RegisterAsync(dto);
                return CreatedAtAction(nameof(Register), new { id = result.RegistrationId }, result); 
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ConflictException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelRegistration(int id)
        {
            try
            {
                var result = await _registrationService.CancelRegistrationAsync(id) ;
                return  Ok(result) ;
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ConflictException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}