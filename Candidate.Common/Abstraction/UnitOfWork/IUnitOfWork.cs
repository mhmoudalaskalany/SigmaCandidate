using System;
using System.Threading.Tasks;
using Candidate.Common.Abstraction.Repository;

namespace Candidate.Common.Abstraction.UnitOfWork
{
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        /// <summary>
        /// Repository Instance In Base Service
        /// </summary>
        IRepository<T> Repository { get; }
        /// <summary>
        /// Save Changes Async
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}
