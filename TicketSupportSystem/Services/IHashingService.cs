namespace TicketSupportSystem.Services;

public interface IHashingService
{
    string Hash(string password);
    HashCheckResult Verify(string password, string hash);
    HashCheckResult DummyHashVerify(string password);
}