using Candidate.Common.DTO.Candidate;


// ReSharper disable once CheckNamespace
namespace Candidate.Application.Mapping
{
    public partial class MappingService
    {
        public void MapCandidate()
        {
            CreateMap<Domain.Entities.Candidate, CandidateDto>()
                .ReverseMap();

            CreateMap<Domain.Entities.Candidate, AddCandidateDto>()
                .ReverseMap();

            CreateMap<Domain.Entities.Candidate, UpdateCandidateDto>()
                .ReverseMap();
        }
    }
}