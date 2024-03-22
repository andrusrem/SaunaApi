using Microsoft.EntityFrameworkCore;

namespace SaunaApi.Models
{
    public class SaunaApiDbContext : DbContext
    {
        public SaunaApiDbContext(DbContextOptions<SaunaApiDbContext> options)
        : base(options)
        {

        }
       public DbSet<User> Users { get; set; }
       public DbSet<BookedTime> BookedTimes { get; set; }
       public DbSet<Order> Orders { get; set; }

    }
}