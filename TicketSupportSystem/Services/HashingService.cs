using Microsoft.AspNetCore.Identity;

namespace TicketSupportSystem.Services;

public class HashingService : IHashingService
{
    private readonly PasswordHasher<object> _hasher = new();

    public string Hash(string password){
        return _hasher.HashPassword(new object(), password);
    }
    
    public HashCheckResult Verify(string password, string hash){
        var result = _hasher.VerifyHashedPassword(new object(), hash, password);
        return result switch
        {
            PasswordVerificationResult.Success => HashCheckResult.Success,
            PasswordVerificationResult.SuccessRehashNeeded => HashCheckResult.SuccessRehashNeeded,
            _ => HashCheckResult.Failed
        };
    }
}