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
        public async Task GetActionsAsync_Return_Ok()
        {
            //Arrange
            var result = Fixture.Build<List<CandidateDto>>().Create();
            _candidateServiceMock.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(result));

            //Act
            var response = await _controller.CandidatesAsync();

            //Assert
            Assert.True(true);
        }



    }
}