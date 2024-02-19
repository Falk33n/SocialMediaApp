using Microsoft.EntityFrameworkCore;

namespace backendSocial.Model
{
    public class BlogPostDbContext : DbContext
    {
        public DbSet<BlogPost> BlogPost { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=blogpostdb.database.windows.net;Database=BlogPostDb;User Id=falk33n;Password=Timpa1234;");
        }
    }
}