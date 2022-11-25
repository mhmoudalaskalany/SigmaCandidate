using Candidate.Common.DTO.Candidate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Candidate.Application.Services.Candidate
{
    public interface ICandidateService
    {
        Task<CandidateDto> GetAsync(string email);
        Task<List<CandidateDto>> GetAllAsync();
        Task<string> AddAsync(AddCandidateDto model);
        Task<string> UpdateAsync(UpdateCandidateDto model);
        Task<string> DeleteAsync(string email);
    }
}
