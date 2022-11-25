using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public async Task<Domain.Entities.Candidate> GetAsync(Guid id)
        {
            return await _context.Candidates.FindAsync(id);
        }

        public async Task<IEnumerable<Domain.Entities.Candidate>> GetAllAsync()
        {
            return await _context.Candidates.AsNoTracking().ToListAsync();
        }

        public async Task<bool> Any(Expression<Func<Domain.Entities.Candidate, bool>> predicate = null) => predicate == null ? await _context.Candidates.AnyAsync() : await _context.Candidates.AnyAsync(predicate);

        public async Task<Guid> AddAsync(Domain.Entities.Candidate newEntity)
        {
            var result = await _context.Candidates.AddAsync(newEntity);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task UpdateAsync(Domain.Entities.Candidate originalEntity, Domain.Entities.Candidate newEntity)
        {
            _context.Candidates.Entry(originalEntity).CurrentValues.SetValues(newEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Domain.Entities.Candidate entity)
        {
            _context.Candidates.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
