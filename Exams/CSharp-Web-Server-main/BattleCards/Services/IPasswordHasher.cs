namespace BattleCards.Services
{
    public interface IPasswordHasher
    {
        string ComputeSha256Hash(string rawData);
    }
}
