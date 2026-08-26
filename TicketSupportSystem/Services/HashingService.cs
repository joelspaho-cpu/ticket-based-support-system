using Microsoft.AspNetCore.Identity;

namespace TicketSupportSystem.Services;

public class HashingService : IHashingService
{
    private readonly PasswordHasher<object> _hasher = new();
    private readonly string DummyHash = "$pbkdf2-sha256$600000$dK8ClGIYvapf8+QMblgQfg==$y9+tHcGxUjE0ANzusrNJVCm73Q6sNBULieLO69jBrHg=";
    
    public HashCheckResult DummyHashVerify(string password){
         var result = _hasher.VerifyHashedPassword(new object(), password, DummyHash);
         return result switch
        {
            PasswordVerificationResult.Success => HashCheckResult.Success,
            PasswordVerificationResult.SuccessRehashNeeded => HashCheckResult.SuccessRehashNeeded,
            _ => HashCheckResult.Failed
        };
    }
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