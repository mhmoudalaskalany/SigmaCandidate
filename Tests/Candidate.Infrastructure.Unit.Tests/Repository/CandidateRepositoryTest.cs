using AutoFixture;
using AutoFixture.Idioms;
using Candidate.Infrastructure.Context;
using Candidate.Infrastructure.DbContextFactory;
using Candidate.Infrastructure.Repository.CandidateRepository;
using Infrastructure.Unit.Tests;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Candidate.Infrastructure.Unit.Tests.Repository
{
    public class CandidateRepositoryTest : AutoFixtureBase
    {
        private readonly Mock<IDbContextFactory> _mockDbContextFactory;
        public CandidateRepositoryTest()
        {
            _mockDbContextFactory = new Mock<IDbContextFactory>();
            Fixture.Register(() => _mockDbContextFactory.Object);
        }

        [Fact]
        public void Check_Dependencies_GuardClause()
        {
            var assertion = new GuardClauseAssertion(Fixture);
            assertion.Verify(typeof(CandidateRepository).GetConstructors());
        }

        [Fact]
        public async Task GetAsync_ReturnItem()
        {
            var entityToAssert = CreateCandidate();
            var context = await SeedAndGetDbContext();
            _mockDbContextFactory.Setup(x => x.CreateCandidateDbContext()).Returns(context);

            var repository = new CandidateRepository(_mockDbContextFactory.Object);

            var entity = await repository.GetAsync(entityToAssert.Email);

            Assert.NotNull(entity);
        }

        [Fact]
        public async Task Any_Return_True()
        {
            var entityToAssert = CreateCandidate();
            var context = await SeedAndGetDbContext();
            _mockDbContextFactory.Setup(x => x.CreateCandidateDbContext()).Returns(context);

            var repository = new CandidateRepository(_mockDbContextFactory.Object);

            var result = await repository.Any(entityToAssert.Email);

            Assert.True(result);
        }

        [Fact]
        public async Task Any_Return_False()
        {
            var entityToAssert = CreateCandidate();
            var context = await SeedAndGetDbContext();
            _mockDbContextFactory.Setup(x => x.CreateCandidateDbContext()).Returns(context);

            var repository = new CandidateRepository(_mockDbContextFactory.Object);

            var entity = await repository.Any("test");

            Assert.False(entity);
        }



        [Fact]
        public async Task AddAsync_ReturnEmail()
        {
            var memoryContext = GetMemoryContextDbContext();
            await memoryContext.ClearData();

            var entityToAdd = CreateCandidate("test2@test.test");
            var context = await SeedAndGetDbContext();
            _mockDbContextFactory.Setup(x => x.CreateCandidateDbContext()).Returns(context);

            var repository = new CandidateRepository(_mockDbContextFactory.Object);

            var email = await repository.AddAsync(entityToAdd);

            Assert.Equal(entityToAdd.Email, email);


            await memoryContext.ClearData();

        }

        [Fact]
        public async Task UpdateAsync_Entity_Is_Updated()
        {
            var entityToUpdate = CreateCandidate();
            var context = await SeedAndGetDbContext();
            _mockDbContextFactory.Setup(x => x.CreateCandidateDbContext()).Returns(context);
            var originalEntity = await context.Candidates.FindAsync(entityToUpdate.Email);
            var repository = new CandidateRepository(_mockDbContextFactory.Object);

            await repository.UpdateAsync(originalEntity, entityToUpdate);

            var entityAfterUpdate = await context.Candidates.FindAsync(entityToUpdate.Email);
            Assert.NotNull(entityAfterUpdate);
            Assert.Equal(entityToUpdate.Email, entityAfterUpdate.Email);
            Assert.Equal(entityToUpdate.PhoneNumber, entityAfterUpdate.PhoneNumber);
            Assert.Equal(entityToUpdate.FirstName, entityAfterUpdate.FirstName);
            Assert.Equal(entityToUpdate.LastName, entityAfterUpdate.LinkedInUrl);
            Assert.Equal(entityToUpdate.From, entityAfterUpdate.From);
            Assert.Equal(entityToUpdate.To, entityAfterUpdate.To);
            Assert.Equal(entityToUpdate.GithubUrl, entityAfterUpdate.GithubUrl);
            Assert.Equal(entityToUpdate.LinkedInUrl, entityAfterUpdate.LinkedInUrl);
            Assert.Equal(entityToUpdate.Comment, entityAfterUpdate.Comment);
        }

        [Fact]
        public async Task DeleteAsync_Entity_Is_Removed()
        {
            var entityToDelete = CreateCandidate("test2@test.test");
            var context = await SeedAndGetDbContext();
            _mockDbContextFactory.Setup(x => x.CreateCandidateDbContext()).Returns(context);

            var repository = new CandidateRepository(_mockDbContextFactory.Object);

            await repository.DeleteAsync(entityToDelete.Email);

            var deleted = await context.Candidates.FindAsync(entityToDelete.Email);

            Assert.Null(deleted);

        }

        [Fact]
        public async Task DeleteAsync_Entity_Is_Null()
        {
            var context = await SeedAndGetDbContext();
            _mockDbContextFactory.Setup(x => x.CreateCandidateDbContext()).Returns(context);

            var repository = new CandidateRepository(_mockDbContextFactory.Object);
            var exist = await context.Candidates.FirstOrDefaultAsync(x =>x.Email =="test@test.test");
            await repository.DeleteAsync("test@test.test");

            Assert.NotNull(exist);

        }




        private async Task<CandidateDbContext> SeedAndGetDbContext()
        {
            var context = new InMemoryDbContext.InMemoryDbContext();
            await context.AddOneCandidate(CreateCandidate());

            return context.MemoryCandidateDbContext;
        }

        private InMemoryDbContext.InMemoryDbContext GetMemoryContextDbContext()
        {
            var memoryContext = new InMemoryDbContext.InMemoryDbContext();

            return memoryContext;
        }

        private Domain.Entities.Candidate CreateCandidate(string email = "test@test.test")
        {
            return new Domain.Entities.Candidate()
            {
                Comment = "test",
                Email = email,
                FirstName = "test",
                LastName = "test",
                From = TimeSpan.MaxValue,
                To = TimeSpan.MaxValue,
                GithubUrl = "test",
                LinkedInUrl = "test",
                PhoneNumber = "123456"
            };
        }
    }
}
