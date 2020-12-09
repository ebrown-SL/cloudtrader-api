using System;

namespace CloudTrader.Api.Domain.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateToken(Guid id);
    }
}