using System.Diagnostics.CodeAnalysis;
using Candidate.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Candidate.Infrastructure.Context
{
    [ExcludeFromCodeCoverage]
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
