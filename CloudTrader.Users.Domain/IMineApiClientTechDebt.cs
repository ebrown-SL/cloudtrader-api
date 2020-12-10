using System;
using System.Threading.Tasks;

namespace CloudTrader.Users.Domain
{
    /// <remark>
    /// TECH DEBT We intend to separate this api client from users domain
    /// </remark>
    public interface IMineApiClientTechDebt
    {
        Task UpdateMineStock(
            Guid mineId,
            int purchaseQuantity
        );
    }
}