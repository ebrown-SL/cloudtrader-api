using CloudTrader.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudTrader.Api.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
