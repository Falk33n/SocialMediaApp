using Microsoft.EntityFrameworkCore;

namespace backendSocial.Model
{
    public class PostDbContext : DbContext
    {
        public DbSet<Post> Post { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=SocialMediaApp;");
        }
    }
}