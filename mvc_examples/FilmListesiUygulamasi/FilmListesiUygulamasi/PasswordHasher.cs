using Microsoft.AspNetCore.Identity;

public class PasswordHasher
{
    private readonly PasswordHasher<string> _passwordHasher;

    public PasswordHasher()
    {
        _passwordHasher = new PasswordHasher<string>();
    }

    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(null, password);
    }

    public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        return _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
    }
}
