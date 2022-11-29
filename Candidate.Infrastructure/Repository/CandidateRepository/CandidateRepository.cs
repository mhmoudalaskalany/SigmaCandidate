using System;
using System.Threading.Tasks;
using Candidate.Common.Abstraction.Repository;
using Candidate.Infrastructure.DbContextFactory;
using Microsoft.EntityFrameworkCore;

namespace Candidate.Infrastructure.Repository.CandidateRepository
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly IDbContextFactory _dbContextFactory;

        public CandidateRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        }

        public virtual async Task<Domain.Entities.Candidate> GetAsync(string email)
        {
            var context = _dbContextFactory.CreateCandidateDbContext();
            return await context.Candidates.FindAsync(email);
        }

        public virtual async Task<bool> Any(string email)
        {
            var context = _dbContextFactory.CreateCandidateDbContext();
            return await context.Candidates.AnyAsync(x => x.Email == email);
        }

        public virtual async Task<string> AddAsync(Domain.Entities.Candidate newEntity)
        {
            var context = _dbContextFactory.CreateCandidateDbContext();
            var result = await context.Candidates.AddAsync(newEntity);
            await context.SaveChangesAsync();
            return result.Entity.Email;
        }

        public virtual async Task UpdateAsync(Domain.Entities.Candidate originalEntity, Domain.Entities.Candidate newEntity)
        {
            var context = _dbContextFactory.CreateCandidateDbContext();
            context.Candidates.Entry(originalEntity).CurrentValues.SetValues(newEntity);
            await context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(string email)
        {
            var context = _dbContextFactory.CreateCandidateDbContext();
            var entity = await context.Candidates.FirstOrDefaultAsync(x => x.Email == email);
            if (entity != null)
            {
                context.Candidates.Remove(entity);
                await context.SaveChangesAsync();
            }

        }
    }
}
