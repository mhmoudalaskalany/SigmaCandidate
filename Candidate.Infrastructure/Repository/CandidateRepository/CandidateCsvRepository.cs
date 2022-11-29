using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Candidate.Common.Abstraction.Repository;
using Candidate.Common.Exceptions;
using Candidate.Common.FileHelper;
using Microsoft.AspNetCore.Hosting;

namespace Candidate.Infrastructure.Repository.CandidateRepository
{
    [ExcludeFromCodeCoverage]
    public class CandidateCsvRepository : ICandidateRepository
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _path;

        public CandidateCsvRepository(IWebHostEnvironment env)
        {
            _env = env;
            _path = $"{_env.ContentRootPath}\\Files\\Candidate.csv";
        }

        public virtual async Task<Domain.Entities.Candidate> GetAsync(string email)
        {
            var line = await FileHelper.GetSingleLine(_path, email);
            if (line == null)
            {
                throw new EntityNotFoundException(email);
            }
            var candidate = CsvRowParser.ParseRow(line);
            return candidate;
        }

        public virtual async Task<bool> Any(string email)
        {
            return await FileHelper.IsExist(_path, email);
        }

        public virtual async Task<string> AddAsync(Domain.Entities.Candidate newEntity)
        {
            await FileHelper.WriteCsv(newEntity, _path);
            return newEntity.Email;
        }

        public virtual async Task UpdateAsync(Domain.Entities.Candidate originalEntity, Domain.Entities.Candidate newEntity)
        {
            await FileHelper.UpdateLine(newEntity, _path , newEntity.Email);
        }

        public virtual async Task DeleteAsync(string email)
        {
            await FileHelper.DeleteLine(_path, email);
        }





    }
}
