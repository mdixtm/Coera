using Microsoft.EntityFrameworkCore;
using TestProject.Models;

namespace TestProject
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Candidate>()
                .HasKey(c => c.Email);
        }
    }
}
