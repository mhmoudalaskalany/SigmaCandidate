using AutoFixture;
using AutoFixture.Idioms;
using AutoMapper;
using Candidate.Application.Mapping;
using Candidate.Application.Services.Candidate;
using Candidate.Common.Abstraction.Repository;
using Candidate.Common.DTO.Candidate;
using Candidate.Common.Exceptions;
using Candidate.Domain.Enum;
using Candidate.Infrastructure.Repository.CandidateRepository;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Candidate.Application.Unit.Tests.Service
{
    public class CandidateServiceWithDatabaseTests : AutoFixtureBase
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<Func<InfrastructureType, ICandidateRepository>> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<CandidateRepository> _databaseRepositoryMock;
        private readonly MapperConfiguration _mapperConfig;
        public CandidateServiceWithDatabaseTests()
        {

            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.SetupGet(x => x[It.Is<string>(s => s == "InfrastructureType")]).Returns("1");
            Fixture.Register(() => _configurationMock.Object);

            _databaseRepositoryMock = new Mock<CandidateRepository>();
            _repositoryMock = new Mock<Func<InfrastructureType, ICandidateRepository>>();
            _repositoryMock.Setup(e => e.Invoke(InfrastructureType.Database)).Returns(_databaseRepositoryMock.Object);

            _mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingService>());
            Fixture.Register(() => _mapperConfig.CreateMapper());
            _mapperMock = new Mock<IMapper>();
            Fixture.Register(() => _mapperMock.Object);

        }
        [Fact]
        public void Check_AutoMapper_Configuration()
        {
            _mapperConfig.AssertConfigurationIsValid();
        }

        [Fact]
        public void Check_Dependencies_GuardClause()
        {
            var assertion = new GuardClauseAssertion(Fixture);
            assertion.Verify(typeof(CandidateService).GetConstructors());
        }

        [Fact]
        public async Task GetAsync_ReturnItem()
        {

            // arrange
            var entity = Fixture.Build<Domain.Entities.Candidate>().With(e => e.Email, "test@test.test").Create();
            var mapped = Fixture.Build<CandidateDto>().With(e => e.Email, "test@test.test").Create();
            

            _databaseRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(entity);

            _mapperMock.Setup(x => x.Map<Domain.Entities.Candidate, CandidateDto>(It.IsAny<Domain.Entities.Candidate>()))
                .Returns(mapped);
            var service = new CandidateService(_mapperMock.Object, _repositoryMock.Object, _configurationMock.Object);
            // act
            var result = await service.GetAsync("test@test.test");

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAsync_ThrowsException()
        {
            Domain.Entities.Candidate? entity = null;

            _databaseRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(entity);

            var service = new CandidateService(_mapperMock.Object, _repositoryMock.Object, _configurationMock.Object);

            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.GetAsync("test@test.test"));
        }


        [Fact]
        public async Task AddAsync_ReturnEmail()
        {

            // arrange
            var isExist = false;
            var email = "test@test.test";
            var dto = Fixture.Build<AddCandidateDto>().With(e => e.Email, email).Create();
            var entity = Fixture.Build<Domain.Entities.Candidate>().With(e => e.Email, email).Create();
            

            _databaseRepositoryMock.Setup(x => x.Any(It.IsAny<string>())).ReturnsAsync(isExist);

            _mapperMock.Setup(x => x.Map<AddCandidateDto, Domain.Entities.Candidate>(It.IsAny<AddCandidateDto>()))
                .Returns(entity);

            _databaseRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.Candidate>())).ReturnsAsync(email);

            var service = new CandidateService(_mapperMock.Object, _repositoryMock.Object, _configurationMock.Object);
            // act
            var result = await service.AddAsync(dto);

            // assert
            Assert.Equal(email, result);
        }

        [Fact]
        public async Task AddAsync_ThrowsException()
        {

            // arrange
            var isExist = true;
            var email = "test@test.test";
            var dto = Fixture.Build<AddCandidateDto>().With(e => e.Email, email).Create();


            _databaseRepositoryMock.Setup(x => x.Any(It.IsAny<string>())).ReturnsAsync(isExist);


            var service = new CandidateService(_mapperMock.Object, _repositoryMock.Object, _configurationMock.Object);
            await Assert.ThrowsAsync<BusinessException>(() => service.AddAsync(dto));
        }


        [Fact]
        public async Task UpdateAsync_ReturnEmail()
        {

            // arrange
            var email = "test@test.test";
            var dto = Fixture.Build<UpdateCandidateDto>().With(e => e.Email, email).Create();
            var entity = Fixture.Build<Domain.Entities.Candidate>().With(e => e.Email, email).Create();
            

            _databaseRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(entity);

            _mapperMock.Setup(x => x.Map(It.IsAny<UpdateCandidateDto>(), It.IsAny<Domain.Entities.Candidate>()))
                .Returns(entity);


            var service = new CandidateService(_mapperMock.Object, _repositoryMock.Object, _configurationMock.Object);
            // act
            var result = await service.UpdateAsync(dto);



            // assert
            _databaseRepositoryMock.Verify(e => e.UpdateAsync(It.IsAny<Domain.Entities.Candidate>(), It.IsAny<Domain.Entities.Candidate>()), Times.Once);
            Assert.Equal(email, result);
        }

        [Fact]
        public async Task UpdateAsync_ThrowsException()
        {

            // arrange
            var email = "test@test.test";
            Domain.Entities.Candidate? entity = null;
            var dto = Fixture.Build<UpdateCandidateDto>().With(e => e.Email, email).Create();


            _databaseRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(entity);


            var service = new CandidateService(_mapperMock.Object, _repositoryMock.Object, _configurationMock.Object);
            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.UpdateAsync(dto));
        }


        [Fact]
        public async Task DeleteAsync_ReturnEmail_And_Delete_Is_Called()
        {

            // arrange
            var email = "test@test.test";
            

            var service = new CandidateService(_mapperMock.Object, _repositoryMock.Object, _configurationMock.Object);
            // act
            var result = await service.DeleteAsync(email);

            // assert
            _databaseRepositoryMock.Verify(e => e.DeleteAsync(It.IsAny<string>()), Times.Once);
            Assert.Equal(email, result);
        }
    }
}
