using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Candidate.Common.Abstraction.UnitOfWork;
using Candidate.Common.DTO.Candidate;

namespace Candidate.Application.Services.Candidate
{
    public class CandidateService : ICandidateService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<global::Candidate.Domain.Entities.Candidate> _uow;
        public CandidateService(IUnitOfWork<global::Candidate.Domain.Entities.Candidate> uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        public async Task<List<CandidateDto>> GetAllAsync()
        {
            var entities = await _uow.Repository.GetAllAsync();
            var data = _mapper.Map<IEnumerable<global::Candidate.Domain.Entities.Candidate>, List<CandidateDto>>(entities);
            return data;
        }

    }
}
