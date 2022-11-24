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


        public async Task<CandidateDto> GetAsync(Guid id)
        {
            var entity = await _uow.Repository.GetAsync(id);
            var data = _mapper.Map<Domain.Entities.Candidate, CandidateDto>(entity);
            return data;
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

        public async Task<Guid> UpdateAsync(UpdateCandidateDto model)
        {
            var entityToUpdate = await _uow.Repository.GetAsync(model.Id);
            var newEntity = _mapper.Map(model , entityToUpdate);
            _uow.Repository.Update(entityToUpdate , newEntity);
            await _uow.SaveChangesAsync();
            return entityToUpdate.Id;

        }

    }
}
