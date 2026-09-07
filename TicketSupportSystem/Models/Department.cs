using System.ComponentModel.DataAnnotations;

namespace TicketSupportSystem.Models;

public class Department
{
    public int DepartmentID {get; set;}
    [Required, MaxLength(50)]
    public required string Name {get; set;}
    [Required, MaxLength(100)]
    public required string Description {get; set;}
    public Level Level {get; set;}
}