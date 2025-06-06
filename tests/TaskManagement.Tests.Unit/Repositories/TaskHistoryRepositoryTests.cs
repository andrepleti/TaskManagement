using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Infrastructure.Context;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagement.Tests.Unit.Repositories
{
    public class TaskHistoryRepositoryTests
    {
        private readonly DataBaseContext _context;
        private readonly ITaskHistoryRepository _repository;

        public TaskHistoryRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            _context = new DataBaseContext(options);
            _repository = new TaskHistoryRepository(_context);
        }

        [Fact]
        public void Add_Should_Add_TaskHistory_To_Database()
        {
            // Arrange
            var taskHistory = new TaskHistory
            {
                Id = 1,
                TaskId = 1,
                ChangedField = "Title",
                OldValue = "Old Title",
                NewValue = "New Title",
                ChangedAt = DateTime.UtcNow,
                ChangedByUserId = 123
            };

            // Act
            _repository.Add(taskHistory);
            _repository.Commit();

            // Assert
            var result = _context.Set<TaskHistory>().Find(1);
            Assert.NotNull(result);
            Assert.Equal("New Title", result.NewValue);
        }

        [Fact]
        public void GetListBy_Should_Return_TaskHistories_For_Task()
        {
            // Arrange
            var history = new TaskHistory { Id = 2, TaskId = 27, ChangedField = "Status" };
            _context.Entry(history).State = EntityState.Added;
            var history2 = new TaskHistory { Id = 3, TaskId = 28, ChangedField = "Description" };
            _context.Entry(history2).State = EntityState.Added;
            _context.SaveChanges();

            // Act
            var result = _repository.GetListBy(27);

            // Assert
            Assert.Single(result);
            Assert.Equal("Status", result.First().ChangedField);
        }
    }
}
