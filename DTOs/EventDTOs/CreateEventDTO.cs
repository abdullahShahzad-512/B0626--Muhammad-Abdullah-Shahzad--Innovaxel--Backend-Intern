using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI.DTOs.EventDTOs
{
    public class CreateEventDTO
    {
        [Required]

        public string Name { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Total Seats must be greater than zero")]
        public int TotalSeats { get; set; }
        [Required]

        public DateTime Date { get; set; }
    }
}
