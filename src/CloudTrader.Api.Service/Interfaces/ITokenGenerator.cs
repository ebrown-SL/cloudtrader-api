using System;

namespace CloudTrader.Api.Service.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateToken(Guid id);
    }
}
