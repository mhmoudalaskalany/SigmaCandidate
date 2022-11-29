using Candidate.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Candidate.Infrastructure.Unit.Tests.InMemoryDbContext
{
    public class InMemoryDbContext : IDisposable
    {
        public CandidateDbContext MemoryCandidateDbContext { get; set; }

        public InMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<CandidateDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCandidateDb")
                .Options;
            MemoryCandidateDbContext = new CandidateDbContext(options);
        }

        public async Task AddOneCandidate(Domain.Entities.Candidate candidate)
        {
            var exist = await MemoryCandidateDbContext.Candidates.SingleOrDefaultAsync(x => x.Email == candidate.Email);
            if (exist == null)
            {
                await MemoryCandidateDbContext.Candidates.AddAsync(candidate);
                await MemoryCandidateDbContext.SaveChangesAsync();
            }
        }

        public async Task ClearData()
        {
            var entities = await MemoryCandidateDbContext.Candidates.ToListAsync();
            MemoryCandidateDbContext.Candidates.RemoveRange(entities);
            await MemoryCandidateDbContext.SaveChangesAsync();
        }
        public async void Dispose()
        {
            await ClearData();
            MemoryCandidateDbContext = null;

        }
    }
}
