using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Candidate.Common.Abstraction.Repository;
using Candidate.Common.DTO.Candidate;

namespace Candidate.Application.Services.Candidate
{
    public class CandidateService : ICandidateService
    {
        private readonly IMapper _mapper;
        private readonly ICandidateRepository _candidateRepository;
        public CandidateService( IMapper mapper, ICandidateRepository candidateRepository)
        {
            _mapper = mapper;
            _candidateRepository = candidateRepository;
        }


        public async Task<CandidateDto> GetAsync(Guid id)
        {
            var entity = await _candidateRepository.GetAsync(id);
            var data = _mapper.Map<Domain.Entities.Candidate, CandidateDto>(entity);
            return data;
        }

        public async Task<List<CandidateDto>> GetAllAsync()
        {
            var entities = await _candidateRepository.GetAllAsync();
            var data = _mapper.Map<IEnumerable<Domain.Entities.Candidate>, List<CandidateDto>>(entities);
            return data;
        }

        public async Task<Guid> AddAsync(AddCandidateDto model)
        {
            var entity = _mapper.Map<Domain.Entities.Candidate>(model);
            await _candidateRepository.AddAsync(entity);
            return entity.Id;

        }

        public async Task<Guid> UpdateAsync(UpdateCandidateDto model)
        {
            var entityToUpdate = await _candidateRepository.GetAsync(model.Id);
            var newEntity = _mapper.Map(model , entityToUpdate);
            await _candidateRepository.UpdateAsync(entityToUpdate , newEntity);
            return entityToUpdate.Id;
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            var entityToDelete = await _candidateRepository.GetAsync(id);
            await _candidateRepository.DeleteAsync(entityToDelete);
            return id;

        }

    }
}
