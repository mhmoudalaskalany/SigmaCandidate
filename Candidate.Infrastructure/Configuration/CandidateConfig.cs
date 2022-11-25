using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Candidate.Infrastructure.Configuration
{
    public class CandidateConfig : IEntityTypeConfiguration<Domain.Entities.Candidate>
    {
        public virtual void Configure(EntityTypeBuilder<Domain.Entities.Candidate> builder)
        {
            builder.HasKey(e => e.Email);
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Comment).IsRequired();
        }
    }
}
