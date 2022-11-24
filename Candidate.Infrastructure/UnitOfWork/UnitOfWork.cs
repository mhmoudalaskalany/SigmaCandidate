using System;
using System.Threading.Tasks;
using Candidate.Common.Abstraction.Repository;
using Candidate.Common.Abstraction.UnitOfWork;
using Candidate.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Candidate.Infrastructure.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class 
    {
        private DbContext _context;
        public IRepository<T> Repository { get; }
        public UnitOfWork(DbContext context)
        {
            _context = context;
            Repository = new Repository<T>(_context);
        }

 
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (_context == null)
            {
                return;
            }

            _context.Dispose();
            _context = null;
        }
        

    }
}