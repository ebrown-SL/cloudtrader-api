using System;

namespace CloudTrader.Api.Auth
{
    public interface ITokenGenerator
    {
        string GenerateToken(Guid id);
    }
}