using System;
using Candidate.Common.DTO.Candidate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Candidate.Application.Services.Candidate
{
    public interface ICandidateService
    {
        Task<List<CandidateDto>> GetAllAsync();
        Task<Guid> AddAsync(AddCandidateDto model);
    }
}
