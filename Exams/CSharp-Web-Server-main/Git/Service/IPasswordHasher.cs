
namespace Git.Service
{
    public interface IPasswordHasher
    {
        string ComputeSha256Hash(string rawData);
    }
}
