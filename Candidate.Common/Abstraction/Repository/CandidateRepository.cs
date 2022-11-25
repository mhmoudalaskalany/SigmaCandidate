using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Candidate.Common.Abstraction.Repository
{
    public interface ICandidateRepository
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        Task<Domain.Entities.Candidate> GetAsync(params object[] keys);

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Domain.Entities.Candidate>> GetAllAsync(bool disableTracking = true);

        /// <summary>
        /// Any
        /// </summary>
        /// <returns></returns>
        Task<bool> Any(Expression<Func<Domain.Entities.Candidate, bool>> predicate = null);


        /// <summary>
        /// Add
        /// </summary>
        /// <returns></returns>
        Task<Guid> AddAsync(Domain.Entities.Candidate newEntity);


        /// <summary>
        /// Update
        /// </summary>
        Task UpdateAsync(Domain.Entities.Candidate originalEntity, Domain.Entities.Candidate newEntity);


        /// <summary>
        /// Remove
        /// </summary>
        Task DeleteAsync(Domain.Entities.Candidate entity);
    }
}
