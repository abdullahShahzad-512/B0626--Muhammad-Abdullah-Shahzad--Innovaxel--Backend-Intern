using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Registration
{
    public string Username { get; set; }
    [ForgeinKey]
    public int EventID {  get; set; }
    public DateTime RegisteredAt { get; set; }
    public bool isCancelled { get; set; }=false;
}
