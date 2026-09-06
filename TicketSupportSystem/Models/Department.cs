using System.ComponentModel.DataAnnotations;

namespace TicketSupportSystem.Models;

public class Department
{
    public int DepartmentID {get; set;}
    public required string Name {get; set;}
    public required string Description {get; set;}
}