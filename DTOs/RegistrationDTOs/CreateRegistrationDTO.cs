using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI.DTOs.RegistrationDTOs
{
    public class CreateRegistrationDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public int EventId { get; set; }
    }
}
