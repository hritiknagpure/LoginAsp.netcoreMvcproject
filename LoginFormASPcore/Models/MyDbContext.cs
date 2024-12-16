using Microsoft.EntityFrameworkCore;

namespace LoginFormASPcore.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        // Define your DbSet properties here
        public DbSet<UserTbl> UserTbls { get; set; }
    }
}
