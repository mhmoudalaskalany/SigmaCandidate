using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Candidate.Common.DTO.Candidate
{
    [ExcludeFromCodeCoverage]
    public class AddCandidateDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
      

        public string LinkedInUrl { get; set; }

        public string GithubUrl { get; set; }

        public TimeSpan From { get; set; }

        public TimeSpan To { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}
