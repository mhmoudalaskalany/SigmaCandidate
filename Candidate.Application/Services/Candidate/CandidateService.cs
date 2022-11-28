using System;
using System.Threading.Tasks;
using AutoMapper;
using Candidate.Common.Abstraction.Repository;
using Candidate.Common.DTO.Candidate;
using Candidate.Common.Exceptions;
using Candidate.Domain.Enum;
using Microsoft.Extensions.Configuration;

namespace Candidate.Application.Services.Candidate
{
    public class CandidateService : ICandidateService
    {
        private readonly IMapper _mapper;
        private readonly Func<string, ICandidateRepository> _candidateRepository;
        private readonly IConfiguration _configuration;
        private readonly InfrastructureType _infrastructureType;
        public CandidateService(IMapper mapper, Func<string, ICandidateRepository> candidateRepository, IConfiguration configuration)
        {
            _mapper = mapper ?? throw new ArgumentNullException(); ;
            _candidateRepository = candidateRepository ?? throw new ArgumentNullException(); ;
            _configuration = configuration ?? throw new ArgumentNullException();
            _infrastructureType = int.Parse(_configuration["InfrastructureType"] ?? throw new InvalidOperationException()) == 0
                ? InfrastructureType.Csv
                : InfrastructureType.Database;
        }


        public async Task<CandidateDto> GetAsync(string email)
        {
            var entity = await _candidateRepository(_infrastructureType.ToString()).GetAsync(email);
            if (entity == null)
            {
                throw new EntityNotFoundException(email);
            }
            var data = _mapper.Map<Domain.Entities.Candidate, CandidateDto>(entity);
            return data;
        }

        public async Task<string> AddAsync(AddCandidateDto model)
        {
            var isExist = await _candidateRepository(_infrastructureType.ToString()).Any(model.Email);
            if (isExist)
            {
                throw new BusinessException("Email Already Exist", null);
            }
            var entity = _mapper.Map<Domain.Entities.Candidate>(model);
            await _candidateRepository(_infrastructureType.ToString()).AddAsync(entity);
            return entity.Email;

        }

        public async Task<string> UpdateAsync(UpdateCandidateDto model)
        {
            var entityToUpdate = await _candidateRepository(_infrastructureType.ToString()).GetAsync(model.Email);
            if (entityToUpdate == null)
            {
                throw new EntityNotFoundException(model.Email);
            }
            var newEntity = _mapper.Map(model, entityToUpdate);
            await _candidateRepository(_infrastructureType.ToString()).UpdateAsync(entityToUpdate, newEntity);
            return entityToUpdate.Email;
        }

        public async Task<string> DeleteAsync(string email)
        {
            await _candidateRepository(_infrastructureType.ToString()).DeleteAsync(email);
            return email;

        }

    }
}
