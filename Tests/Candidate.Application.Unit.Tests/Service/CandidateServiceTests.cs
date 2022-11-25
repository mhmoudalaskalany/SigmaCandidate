using AutoFixture;
using AutoMapper;
using Candidate.Application.Services.Candidate;
using Candidate.Common.Abstraction.Repository;
using Candidate.Common.DTO.Candidate;
using Moq;

namespace Candidate.Application.Unit.Tests.Service
{
    public class CandidateServiceTests : AutoFixtureBase
    {
        private readonly Mock<ICandidateRepository> _candidateRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        public CandidateServiceTests()
        {
            _candidateRepositoryMock = new Mock<ICandidateRepository>();
            Fixture.Register(() => _candidateRepositoryMock.Object);

            _mapperMock = new Mock<IMapper>();
            Fixture.Register(() => _mapperMock.Object);
            
        }

        [Fact]
        public async Task GetAllAsync_ReturnList()
        {
            // arrange
            var entities = Fixture.Build<Domain.Entities.Candidate>().CreateMany();
            var mapped = Fixture.Build<CandidateDto>().CreateMany().ToList();

            _candidateRepositoryMock.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(entities));

            _mapperMock.Setup(x => x.Map<IEnumerable<Domain.Entities.Candidate>, List<CandidateDto>>(It.IsAny<IEnumerable<Domain.Entities.Candidate>>()))
                .Returns(mapped);
            var service = Fixture.Create<CandidateService>();
            // act
            var result = await service.GetAllAsync();

            // assert
            Assert.True(result.Any());
        }
    }
}
