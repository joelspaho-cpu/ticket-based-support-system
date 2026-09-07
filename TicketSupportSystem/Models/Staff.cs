using System.ComponentModel.DataAnnotations;

namespace TicketSupportSystem.Models;

public class Staff {
    public int StaffID {get; set;}
    [Required, MaxLength(50)]
    public required string DisplayName {get; set;}
    [Required, MaxLength(254), EmailAddress]
    public required string Email {get; set;}
    [Required, MaxLength(100)]
    public required string PasswordHash {get; set;}
    public StaffRole Role {get; set;}
    public Level? Level {get; set;}
    [MaxLength(100)]
    public string? Signature {get; set;}
    public DateTime CreatedAt {get; set;}

}