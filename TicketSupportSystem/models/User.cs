using System.ComponentModel.DataAnnotations;

public class User
{
    public int UserID {get; set;}
    [Required]
    public required string DisplayName {get; set;}
    [Required]
    public required string Email {get; set;}
    public required string PasswordHash {get; set;}
    public bool Has2fa {get; set;}
    [Required]
    public required string Region {get; set;}
    [Required]
    public required string Language {get; set;}
    public DateTime CreatedAt {get; set;}
    public bool RemainSignedIn {get; set;}

}