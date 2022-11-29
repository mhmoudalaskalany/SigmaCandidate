using System.Threading.Tasks;

namespace Candidate.Common.Abstraction.Repository
{
    public interface ICandidateRepository
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        Task<Domain.Entities.Candidate> GetAsync(string email);

        /// <summary>
        /// Any
        /// </summary>
        /// <returns></returns>
        Task<bool> Any(string email);


        /// <summary>
        /// Add
        /// </summary>
        /// <returns></returns>
        Task<string> AddAsync(Domain.Entities.Candidate newEntity);


        /// <summary>
        /// Update
        /// </summary>
        Task UpdateAsync(Domain.Entities.Candidate originalEntity, Domain.Entities.Candidate newEntity);


        /// <summary>
        /// Remove
        /// </summary>
        Task DeleteAsync(string email);
    }
}
