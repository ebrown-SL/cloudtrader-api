namespace CloudTrader.Api.Service.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateToken(int id);
        int DecodeToken(string tokenString);
    }
}
