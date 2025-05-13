namespace trSys.Interfaces
{
    public interface IPasswordHasher
    {
        string CreateHash(string password);
        bool VerifyHash(string inputPassword, string storedHash);
    }
}
