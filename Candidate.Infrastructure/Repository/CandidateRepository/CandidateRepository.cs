using System.Threading.Tasks;
using Candidate.Common.Abstraction.Repository;
using Candidate.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Candidate.Infrastructure.Repository.CandidateRepository
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly CandidateDbContext _context;

        public CandidateRepository(CandidateDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Candidate> GetAsync(string email)
        {
            return await _context.Candidates.FindAsync(email);
        }

        public async Task<bool> Any(string email) =>   await _context.Candidates.AnyAsync(x => x.Email == email);

        public async Task<string> AddAsync(Domain.Entities.Candidate newEntity)
        {
            var result = await _context.Candidates.AddAsync(newEntity);
            await _context.SaveChangesAsync();
            return result.Entity.Email;
        }

        public async Task UpdateAsync(Domain.Entities.Candidate originalEntity, Domain.Entities.Candidate newEntity)
        {
            _context.Candidates.Entry(originalEntity).CurrentValues.SetValues(newEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string email)
        {
            var entity = await _context.Candidates.FirstOrDefaultAsync(x => x.Email == email);
            if (entity != null)
            {
                _context.Candidates.Remove(entity);
                await _context.SaveChangesAsync();
            }
            
        }
    }
}
