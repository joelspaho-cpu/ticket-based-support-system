namespace TicketSupportSystem.Models;

public enum TicketQuery
    {
        GeneralQuestion = 0, // L1 scope
        Billing = 1, // L1 Scope
        Account = 2, // L2 Scope (plus everything L1 has as well)
        Product = 3, // L2 Scope (plus everything L1 has as well)
        FeedbackComplaint = 4, // L2 Scope + Engineering 
        Sales = 5 // L1 Scope
    }