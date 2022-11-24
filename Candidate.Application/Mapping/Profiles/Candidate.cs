using Candidate.Common.DTO.Candidate;


// ReSharper disable once CheckNamespace
namespace Candidate.Application.Mapping
{
    public partial class MappingService
    {
        public void MapCandidate()
        {
            CreateMap<Candidate.Domain.Entities.Candidate, CandidateDto>()
                .ReverseMap();

            CreateMap<Candidate.Domain.Entities.Candidate, AddCandidateDto>()
                .ReverseMap();
        }
    }
}