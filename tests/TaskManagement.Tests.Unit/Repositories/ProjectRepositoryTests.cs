using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Infrastructure.Context;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagement.Tests.Unit.Repositories
{
    public class ProjectRepositoryTests
    {
        private readonly DataBaseContext _context;
        private readonly IProjectRepository _repository;

        public ProjectRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            _context = new DataBaseContext(options);
            _repository = new ProjectRepository(_context);
        }

        [Fact]
        public void Add_Should_Add_Project_To_Database()
        {
            // Arrange
            var project = new Project { Id = 1, Title = "Test Project", UserId = 1 };

            // Act
            _repository.Add(project);
            _repository.Commit();

            // Assert
            var result = _context.Set<Project>().Find(1);
            Assert.NotNull(result);
            Assert.Equal("Test Project", result.Title);
        }

        [Fact]
        public void GetListBy_Should_Return_Projects_For_User()
        {
            // Arrange
            _context.Set<Project>().Add(new Project { Id = 2, Title = "Project 1", UserId = 1 });
            _context.Set<Project>().Add(new Project { Id = 3, Title = "Project 2", UserId = 2 });
            _context.SaveChanges();

            // Act
            var result = _repository.GetListBy(1);

            // Assert
            Assert.Single(result);
            Assert.Equal("Project 1", result.First().Title);
        }

        [Fact]
        public void GetObjectBy_Should_Return_Project_By_Id()
        {
            // Arrange
            var project = new Project { Id = 4, Title = "Specific Project", UserId = 1 };
            _context.Set<Project>().Add(project);
            _context.SaveChanges();

            // Act
            var result = _repository.GetObjectBy(4);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Specific Project", result.Title);
        }

        [Fact]
        public void Delete_Should_Remove_Project_From_Database()
        {
            // Arrange
            var project = new Project { Id = 5, Title = "To Delete", UserId = 1 };
            _context.Set<Project>().Add(project);
            _context.SaveChanges();

            // Act
            _repository.Delete(project);
            _repository.Commit();

            // Assert
            var result = _context.Set<Project>().Find(5);
            Assert.Null(result);
        }
    }
}
