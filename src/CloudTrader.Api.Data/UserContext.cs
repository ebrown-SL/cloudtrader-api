using Microsoft.EntityFrameworkCore;

namespace CloudTrader.Api.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<UserDbModel> Users { get; set; }
    }
}
