using EventRegistrationAPI.DTOs.EventDTOs;
using EventRegistrationAPI.Exceptions;
using EventRegistrationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDTO dto)
        {
            try
            {
                var result = await _eventService.CreateEventAsync(dto);
                return CreatedAtAction(nameof(CreateEvent), new { id = result.EventId }, result); 
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

        [HttpGet]
        public async Task<IActionResult> GetAllEvents(
            [FromQuery] bool upcomingOnly = false,
            [FromQuery] bool sortByDate = false)
        {
            try
            {
                var result = await _eventService.GetAllEventsAsync(upcomingOnly,sortByDate);
                return Ok(result) ;
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
