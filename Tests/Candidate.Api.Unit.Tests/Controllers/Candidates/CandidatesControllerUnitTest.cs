using AutoFixture;
using Candidate.Api.Controllers;
using Candidate.Application.Services.Candidate;
using Candidate.Common.DTO.Candidate;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Candidate.Api.Unit.Tests.Controllers.Candidates
{
    public class CandidatesControllerUnitTest : AutoFixtureBase
    {
        private readonly Mock<ICandidateService> _candidateServiceMock;
        private readonly CandidatesController _controller;
        private const string Email = "test@test.test";
        public CandidatesControllerUnitTest()
        {
            _candidateServiceMock = new Mock<ICandidateService>();
            Fixture.Register(() => _candidateServiceMock.Object);
            _controller = new CandidatesController(_candidateServiceMock.Object);
        }

        [Fact]
        public async Task CandidateAsync_Return_Ok()
        {
            //Arrange
            var result = Fixture.Build<CandidateDto>().With(e => e.Email , Email).Create();
            _candidateServiceMock.Setup(x => x.GetAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(result));

            //Act
            var response = await _controller.CandidateAsync(email:Email);
            var okResult = response as OkObjectResult;
       //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task PostAsync_Return_Ok()
        {
            //Arrange
            var input = Fixture.Build<AddCandidateDto>().With(e => e.Email , Email).Create();
            _candidateServiceMock.Setup(x => x.AddAsync(It.IsAny<AddCandidateDto>()))
                .Returns(Task.FromResult(Email));

            //Act
            var response = await _controller.PostAsync(input);

            var okResult = response as OkObjectResult;
            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }


        [Fact]
        public async Task PutAsync_Return_Ok()
        {
            //Arrange
            var input = Fixture.Build<UpdateCandidateDto>().With(e => e.Email, Email).Create();
            _candidateServiceMock.Setup(x => x.UpdateAsync(It.IsAny<UpdateCandidateDto>()))
                .Returns(Task.FromResult(Email));

            //Act
            var response = await _controller.PutAsync(input);

            var okResult = response as OkObjectResult;
            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }


        [Fact]
        public async Task DeleteAsync_Return_Ok()
        {
            //Arrange
            _candidateServiceMock.Setup(x => x.DeleteAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(Email));

            //Act
            var response = await _controller.DeleteAsync(Email);

            var okResult = response as OkObjectResult;
            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }



    }
}