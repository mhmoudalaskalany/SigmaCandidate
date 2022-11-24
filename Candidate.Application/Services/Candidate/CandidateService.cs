using System;
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
        private readonly IUnitOfWork<Domain.Entities.Candidate> _uow;
        public CandidateService(IUnitOfWork<Domain.Entities.Candidate> uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        public async Task<List<CandidateDto>> GetAllAsync()
        {
            var entities = await _uow.Repository.GetAllAsync();
            var data = _mapper.Map<IEnumerable<Domain.Entities.Candidate>, List<CandidateDto>>(entities);
            return data;
        }

        public async Task<Guid> AddAsync(AddCandidateDto model)
        {
            var entity = _mapper.Map<Domain.Entities.Candidate>(model);
            _uow.Repository.Add(entity);
            await _uow.SaveChangesAsync();
            return entity.Id;

        }

    }
}
