namespace EventRegistrationAPI.DTOs.RegistrationDTOs
{
    public class RegistrationResponseDTO
    {
       public int RegistrationId { get; set; }
        public string Username { get; set; }
        public int EventId { get; set; }
        public DateTime RegisteredAt {  get; set; }
        public bool IsCancelled { get; set; }
    }
}
