using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Event
{
    [Key]
    public int EventId {  get; set; }

    [Required]
    public  string Name{ get; set; }

    [Required,Range(1,int.MaxValue,ErrorMessage="Total Seats must be greater than zero")] 
    public int TotalSeats{ get; set; }

    [Required]
    public DateTime Date{ get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    
}
