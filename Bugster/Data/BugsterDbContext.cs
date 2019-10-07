using Bugster.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bugster.Data
{
    public class BugsterDbContext :
        DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        public BugsterDbContext()
        {
        }

        public BugsterDbContext(DbContextOptions options) 
            : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
