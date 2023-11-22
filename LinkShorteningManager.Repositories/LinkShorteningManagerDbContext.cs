using LinkShorteningManager.Repositories.Extensions;
using LinkShorteningManager.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShorteningManager.Repositories
{
    public class LinkShorteningManagerDbContext : DbContext
    {
        public DbSet<Link> Links { get; set; }



        public LinkShorteningManagerDbContext(DbContextOptions<LinkShorteningManagerDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                assembly: typeof(Link).Assembly);

            modelBuilder.Seed();
        }
    }
}
