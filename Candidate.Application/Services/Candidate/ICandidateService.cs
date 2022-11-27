using Candidate.Common.DTO.Candidate;
using System.Threading.Tasks;

namespace Candidate.Application.Services.Candidate
{
    public interface ICandidateService
    {
        Task<CandidateDto> GetAsync(string email);
        Task<string> AddAsync(AddCandidateDto model);
        Task<string> UpdateAsync(UpdateCandidateDto model);
        Task<string> DeleteAsync(string email);
    }
}
