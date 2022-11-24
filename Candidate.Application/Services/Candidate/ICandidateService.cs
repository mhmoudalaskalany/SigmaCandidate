using System.Collections.Generic;
using System.Threading.Tasks;
using Candidate.Common.DTO.Candidate;

namespace Candidate.Application.Services.Candidate
{
    public interface ICandidateService
    {
        Task<List<CandidateDto>> GetAllAsync();
    }
}
