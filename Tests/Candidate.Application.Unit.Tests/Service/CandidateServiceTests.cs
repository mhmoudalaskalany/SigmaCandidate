using AutoFixture;
using AutoFixture.Idioms;
using AutoMapper;
using Candidate.Application.Mapping;
using Candidate.Application.Services.Candidate;
using Candidate.Common.Abstraction.Repository;
using Candidate.Common.DTO.Candidate;
using Candidate.Common.Exceptions;
using Candidate.Infrastructure.Repository.CandidateRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Candidate.Application.Unit.Tests.Service
{
    public class CandidateServiceTests : AutoFixtureBase
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<Func<string, ICandidateRepository>> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly MapperConfiguration _mapperConfig;
        public CandidateServiceTests()
        {
            
            _configurationMock = new Mock<IConfiguration>();
            Fixture.Register(() => _configurationMock.Object);

            _repositoryMock = new Mock<Func<string, ICandidateRepository>>();

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
                _configurationMock.SetupGet(x => x[It.Is<string>(s => s == "InfrastructureType")]).Returns("0");

               
                var env = new Mock<IWebHostEnvironment>();
                var csvRepositoryMock = new Mock<CandidateCsvRepository>(env.Object);
                _repositoryMock.Setup(e => e.Invoke("Csv")).Returns(csvRepositoryMock.Object);

                csvRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(entity);

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
            _configurationMock.SetupGet(x => x[It.Is<string>(s => s == "InfrastructureType")]).Returns("0");


            var env = new Mock<IWebHostEnvironment>();
            var csvRepositoryMock = new Mock<CandidateCsvRepository>(env.Object);
            _repositoryMock.Setup(e => e.Invoke("Csv")).Returns(csvRepositoryMock.Object);

            csvRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(entity);

            var service = new CandidateService(_mapperMock.Object, _repositoryMock.Object, _configurationMock.Object);
   
            await Assert.ThrowsAsync<EntityNotFoundException>(() =>  service.GetAsync("test@test.test"));
        }


        [Fact]
        public async Task AddAsync_ReturnEmail()
        {

            // arrange
            var isExist = false;
            var email = "test@test.test";
            var dto = Fixture.Build<AddCandidateDto>().With(e => e.Email, email).Create();
            var entity = Fixture.Build<Domain.Entities.Candidate>().With(e => e.Email, email).Create();

            _configurationMock.SetupGet(x => x[It.Is<string>(s => s == "InfrastructureType")]).Returns("0");

            var env = new Mock<IWebHostEnvironment>();
            var csvRepositoryMock = new Mock<CandidateCsvRepository>(env.Object);
            _repositoryMock.Setup(e => e.Invoke("Csv")).Returns(csvRepositoryMock.Object);

            csvRepositoryMock.Setup(x => x.Any(It.IsAny<string>())).ReturnsAsync(isExist);

            _mapperMock.Setup(x => x.Map<AddCandidateDto, Domain.Entities.Candidate>(It.IsAny<AddCandidateDto>()))
                .Returns(entity);

            csvRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.Candidate>())).ReturnsAsync(email);
            
            var service = new CandidateService(_mapperMock.Object, _repositoryMock.Object, _configurationMock.Object);
            // act
            var result = await service.AddAsync(dto);

            // assert
            Assert.Equal(email , result);
        }

        [Fact]
        public async Task AddAsync_ThrowsException()
        {

            // arrange
            var isExist = true;
            var email = "test@test.test";
            var dto = Fixture.Build<AddCandidateDto>().With(e => e.Email , email).Create();
            _configurationMock.SetupGet(x => x[It.Is<string>(s => s == "InfrastructureType")]).Returns("0");

            var env = new Mock<IWebHostEnvironment>();
            var csvRepositoryMock = new Mock<CandidateCsvRepository>(env.Object);
            _repositoryMock.Setup(e => e.Invoke("Csv")).Returns(csvRepositoryMock.Object);

            csvRepositoryMock.Setup(x => x.Any(It.IsAny<string>())).ReturnsAsync(isExist);


            var service = new CandidateService(_mapperMock.Object, _repositoryMock.Object, _configurationMock.Object);
            await Assert.ThrowsAsync<BusinessException>(() => service.AddAsync(dto));
        }
    }
}
