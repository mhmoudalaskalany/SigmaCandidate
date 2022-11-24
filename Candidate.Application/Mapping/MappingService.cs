using AutoMapper;

namespace Candidate.Application.Mapping
{
    public partial class MappingService : Profile
    {
        public MappingService()
        {
            MapCandidate();
        }
    }
}