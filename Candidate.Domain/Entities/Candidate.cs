using System;
using System.Diagnostics.CodeAnalysis;

namespace Candidate.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Candidate
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string LinkedInUrl { get; set; }
        public string GithubUrl { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public string Comment { get; set; }
    }
}