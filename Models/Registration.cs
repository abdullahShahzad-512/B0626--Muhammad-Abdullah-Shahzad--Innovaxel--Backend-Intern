using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Registration
{
    [Key]
    public int RegistrationId { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public int EventId { get; set; }
    [ForeignKey("EventId")]
    public Event Event { get; set; }

    [Required]
    public DateTime RegisteredAt { get; set; } = DateTime.Now;

    [Required]
    public bool isCancelled { get; set; }=false;
}
