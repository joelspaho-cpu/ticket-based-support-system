using System.ComponentModel.DataAnnotations;

namespace TicketSupportSystem.Models;

public class Staff{
    public int StaffID {get; set;}
    public required string DisplayName {get; set;}
    public required string Email {get; set;}
    public required string PasswordHash {get; set;}
    public StaffRole Role {get; set;}
    public Department? Department {get; set;}
    public int DepartmentID {get; set;}
    public required string Signature {get; set;}
    public DateTime CreatedAt {get; set;}
    public required string IPAddress {get; set;}

}