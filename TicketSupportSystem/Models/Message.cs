using System.ComponentModel.DataAnnotations;

namespace TicketSupportSystem.Models;

public class Message
{
    public int MessageID {get; set;}
    [Required, MaxLength(5000)]
    public required string Response {get; set;}
    [MaxLength(500)]
    public string? AttachmentFilePath {get; set;}
    public Ticket? Ticket {get; set;}
    public int TicketID {get; set;}
    public User? ResponseByUser {get; set;}
    public int? ResponseByUserID {get; set;}
    public Staff? ResponseByStaff {get; set;}
    public int? ResponseByStaffID {get; set;}
    public DateTime PostedAt {get; set;}
    public bool IsInternal {get; set;}
    [Required, MaxLength(45)]
    public required string IPAddress {get; set;}
}