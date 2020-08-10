using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CloudTrader.Api.Data
{
    public class UserContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public UserContext(DbContextOptions<UserContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<UserDbModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseCosmos(
                _configuration["CosmosEndpoint"],
                _configuration["CosmosKey"],
                databaseName: "CloudTrader");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("Users");
        }
    }
}
