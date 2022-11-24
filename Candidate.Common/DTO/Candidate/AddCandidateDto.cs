using System;
using System.Diagnostics.CodeAnalysis;

namespace Candidate.Common.DTO.Candidate
{
    [ExcludeFromCodeCoverage]
    public class AddCandidateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string LinkedInUrl { get; set; }
        public string GithubUrl { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
    }
}
