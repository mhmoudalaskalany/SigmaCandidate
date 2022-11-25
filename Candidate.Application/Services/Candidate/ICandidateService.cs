using System;
using Candidate.Common.DTO.Candidate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Candidate.Application.Services.Candidate
{
    public interface ICandidateService
    {
        Task<CandidateDto> GetAsync(Guid id);
        Task<List<CandidateDto>> GetAllAsync();
        Task<Guid> AddAsync(AddCandidateDto model);
        Task<Guid> UpdateAsync(UpdateCandidateDto model);
        Task<Guid> DeleteAsync(Guid id);
    }
}
