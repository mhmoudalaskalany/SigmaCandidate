using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Candidate.Common.Abstraction.Repository;

namespace Candidate.Infrastructure.Repository.CandidateRepository
{
    public class CandidateCsvRepository : ICandidateRepository
    {

        public CandidateCsvRepository()
        {

        }

        public async Task<Domain.Entities.Candidate> GetAsync(Guid id)
        {
           //get from csv
           return new Domain.Entities.Candidate();
        }

        public async Task<IEnumerable<Domain.Entities.Candidate>> GetAllAsync()
        {
            return new List<Domain.Entities.Candidate>();
        }

        public async Task<bool> Any(Expression<Func<Domain.Entities.Candidate, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> AddAsync(Domain.Entities.Candidate newEntity)
        {
           return  Guid.Empty;
        }

        public async Task UpdateAsync(Domain.Entities.Candidate originalEntity, Domain.Entities.Candidate newEntity)
        {
            
        }

        public async Task DeleteAsync(Domain.Entities.Candidate entity)
        {
        }
    }
}
