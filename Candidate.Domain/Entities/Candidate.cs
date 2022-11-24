using System;
using System.Diagnostics.CodeAnalysis;
using Candidate.Domain.Entities.Base;

namespace Candidate.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Candidate : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}