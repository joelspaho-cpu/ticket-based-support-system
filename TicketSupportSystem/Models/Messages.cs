using System.ComponentModel.DataAnnotations;

namespace TicketSupportSystem.Models;

public class Messages
{
    public int MessageID {get; set;}
    public required string Response {get; set;}
    public string? Attachment_file_path {get; set;}
    public Ticket? Ticket {get; set;}
    public int TicketID {get; set;}
    public User? User {get; set;}
    public int ResponseByUserID {get; set;}
    public Staff? Staff {get; set;}
    public int ResponseByStaffID {get; set;}

}