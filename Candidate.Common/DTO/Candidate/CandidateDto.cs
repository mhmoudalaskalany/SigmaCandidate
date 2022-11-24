using System.Diagnostics.CodeAnalysis;

namespace Candidate.Common.DTO.Candidate
{
    [ExcludeFromCodeCoverage]
    public class CandidateDto 
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
