using Microsoft.EntityFrameworkCore;

namespace sample_rails_app_8th_edNT.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Micropost> Microposts { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        // Add other DbSet properties as needed
    }
}
