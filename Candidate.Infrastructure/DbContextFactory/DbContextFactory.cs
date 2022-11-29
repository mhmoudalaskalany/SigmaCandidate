using Candidate.Infrastructure.Context;

namespace Candidate.Infrastructure.DbContextFactory
{
    public interface IDbContextFactory
    {
        CandidateDbContext CreateCandidateDbContext();
    }
}
