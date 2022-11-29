using System.Diagnostics.CodeAnalysis;
using Candidate.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Candidate.Infrastructure.DbContextFactory
{
    [ExcludeFromCodeCoverage]
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IConfiguration _configuration;
        public DbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CandidateDbContext CreateCandidateDbContext()
        {
            var optionBuilder = new DbContextOptionsBuilder<CandidateDbContext>();
            var userInMemory = bool.Parse(_configuration["UseInMemoryDatabase"] ?? string.Empty);

            if (userInMemory)
            {
                optionBuilder.UseInMemoryDatabase("CandidateDb");
            }
            else
            {
                var connection = _configuration["ConnectionStrings:Default"];
                optionBuilder.UseSqlServer(connection);

            }
            return new CandidateDbContext(optionBuilder.Options);
        }
    }
}
