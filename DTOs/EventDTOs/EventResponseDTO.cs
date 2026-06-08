namespace EventRegistrationAPI.DTOs.EventDTOs
{
    public class EventResponseDTO
    {
       public int EventId { get; set; } 
       public string Name { get; set; }
       public int TotalSeats { get; set; }
       public DateTime Date { get; set; }
        public int AvailableSeats { get; set; } 
        public int TotalRegistrations { get; set; } 
    }
}
