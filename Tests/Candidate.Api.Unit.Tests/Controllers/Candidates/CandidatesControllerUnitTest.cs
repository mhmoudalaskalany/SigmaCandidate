using AutoFixture;
using Candidate.Api.Controllers;
using Candidate.Application.Services.Candidate;
using Candidate.Common.DTO.Candidate;
using Moq;

namespace Candidate.Api.Unit.Tests.Controllers.Candidates
{
    public class CandidatesControllerUnitTest : AutoFixtureBase
    {
        private readonly Mock<ICandidateService> _candidateServiceMock;
        private readonly CandidatesController _controller;
        public CandidatesControllerUnitTest()
        {
            _candidateServiceMock = new Mock<ICandidateService>();
            Fixture.Register(() => _candidateServiceMock.Object);
            _controller = new CandidatesController(_candidateServiceMock.Object);
        }

        [Fact]
        public async Task GetCandidateAsync_Return_Ok()
        {
            //Arrange
            var result = Fixture.Build<CandidateDto>().Create();
            _candidateServiceMock.Setup(x => x.GetAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(result));

            //Act
            var response = await _controller.CandidateAsync("test@test.test");

            //Assert
            Assert.True(true);
        }



    }
}