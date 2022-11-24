using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Candidate.Infrastructure.Configuration
{
    public class CandidateConfig : IEntityTypeConfiguration<Domain.Entities.Candidate>
    {
        public virtual void Configure(EntityTypeBuilder<Domain.Entities.Candidate> builder)
        {
           
        }
    }
}
