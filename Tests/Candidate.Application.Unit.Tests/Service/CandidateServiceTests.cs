using AutoFixture;
using AutoMapper;
using Candidate.Application.Services.Candidate;
using Candidate.Common.Abstraction.UnitOfWork;
using Candidate.Common.DTO.Candidate;
using Microsoft.EntityFrameworkCore.Query;
using Moq;

namespace Candidate.Application.Unit.Tests.Service
{
    public class CandidateServiceTests : AutoFixtureBase
    {
        private readonly Mock<IUnitOfWork<Domain.Entities.Candidate>> _uowMock;
        private readonly CandidateService _candidateServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        public CandidateServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork<Domain.Entities.Candidate>>();
            Fixture.Register(() => _uowMock.Object);

            _mapperMock = new Mock<IMapper>();
            Fixture.Register(() => _mapperMock.Object);

            _candidateServiceMock = new CandidateService(_uowMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnList()
        {
            // arrange
            var entities = Fixture.Build<Domain.Entities.Candidate>().CreateMany();
            var mapped = Fixture.Build<CandidateDto>().CreateMany().ToList();

            _uowMock.Setup(x => x.Repository.GetAllAsync(It.IsAny<Func<IQueryable<Domain.Entities.Candidate>, IIncludableQueryable<Domain.Entities.Candidate, object>>>()
                , It.IsAny<bool>())).Returns(Task.FromResult(entities));

            _mapperMock.Setup(x => x.Map<IEnumerable<Domain.Entities.Candidate>, List<CandidateDto>>(It.IsAny<IEnumerable<Domain.Entities.Candidate>>()))
                .Returns(mapped);

            // act
            var result = await _candidateServiceMock.GetAllAsync();

            // assert
            Assert.True(result.Any());
        }
    }
}
