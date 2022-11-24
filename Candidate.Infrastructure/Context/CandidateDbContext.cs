using Candidate.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Candidate.Infrastructure.Context
{
    public class CandidateDbContext : DbContext
    {
        public CandidateDbContext(DbContextOptions<CandidateDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Domain.Entities.Candidate> Candidates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CandidateConfig());
            base.OnModelCreating(modelBuilder);
        }




    }
}
