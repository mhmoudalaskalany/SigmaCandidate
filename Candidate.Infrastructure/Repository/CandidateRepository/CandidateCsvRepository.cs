using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Candidate.Common.Abstraction.Repository;
using Microsoft.AspNetCore.Hosting;

namespace Candidate.Infrastructure.Repository.CandidateRepository
{
    public class CandidateCsvRepository : ICandidateRepository
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _path;
        public CandidateCsvRepository(IWebHostEnvironment env)
        {
            _env = env;
            _path = $"{_env.ContentRootPath}\\Files\\Candidate.csv";
        }

        public async Task<Domain.Entities.Candidate> GetAsync(string email)
        {
            var fileStream = File.OpenRead(_path);
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

        public async Task<string> AddAsync(Domain.Entities.Candidate newEntity)
        {
            await File.WriteAllTextAsync(_path, newEntity.ToString());
            return newEntity.Email;
        }

        public async Task UpdateAsync(Domain.Entities.Candidate originalEntity, Domain.Entities.Candidate newEntity)
        {
            await File.WriteAllTextAsync(_path, newEntity.ToString());
        }

        public async Task DeleteAsync(Domain.Entities.Candidate entity)
        {
        }
    }
}
