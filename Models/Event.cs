using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Event
{
    [Key]
    public int EventId {  get; set; }

    [Required]
    [Unique]
    public  string Name{ get; set; }

    [Range(1,int.MaxValue,ErrorMessage="Total Seats must be greater than zero")] 
    public int TotalSeats{ get; set; }

    public DateTime Date{ get;}
    public DateTime CreatedAt { get; set; }

    public void setDate(DateTime dt)
    {
        if (dt > DateTime.Now)
            Date = dt;
        else
            throw Exception("Event must be in the future");
    }
}
