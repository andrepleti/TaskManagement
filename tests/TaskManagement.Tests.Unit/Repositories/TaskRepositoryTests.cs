using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Infrastructure.Context;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagement.Tests.Unit.Repositories
{
    public class TaskRepositoryTests
    {
        private readonly DataBaseContext _context;
        private readonly ITaskRepository _repository;

        public TaskRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            _context = new DataBaseContext(options);
            _repository = new TaskRepository(_context);
        }

        [Fact]
        public void Add_Should_Add_Task_To_Database()
        {
            // Arrange
            var task = new TaskManagement.Domain.Entities.Task
            {
                Id = 1,
                Title = "Test Task",
                ProjectId = 1,
                Description = "Task Description",
                Status = TaskManagement.Domain.Entities.Task.TaskStatus.Pending,
                Priority = TaskManagement.Domain.Entities.Task.TaskPriority.Low,
                DueDate = DateTime.Now.AddDays(5)
            };

            // Act
            _repository.Add(task);
            _repository.Commit();

            // Assert
            var result = _context.Set<TaskManagement.Domain.Entities.Task>().Find(1);
            Assert.NotNull(result);
            Assert.Equal("Test Task", result.Title);
        }

        [Fact]
        public void GetListBy_Should_Return_Tasks_For_Project()
        {
            // Arrange
            _context.Set<TaskManagement.Domain.Entities.Task>().Add(new TaskManagement.Domain.Entities.Task { Id = 2, Title = "Task 1", ProjectId = 1 });
            _context.Set<TaskManagement.Domain.Entities.Task>().Add(new TaskManagement.Domain.Entities.Task { Id = 3, Title = "Task 2", ProjectId = 2 });
            _context.SaveChanges();

            // Act
            var result = _repository.GetListBy(1);

            // Assert
            Assert.Single(result);
            Assert.Equal("Task 1", result.First().Title);
        }

        [Fact]
        public void GetObjectBy_Should_Return_Task_By_Id()
        {
            // Arrange
            var task = new TaskManagement.Domain.Entities.Task { Id = 4, Title = "Specific Task", ProjectId = 1 };
            _context.Set<TaskManagement.Domain.Entities.Task>().Add(task);
            _context.SaveChanges();

            // Act
            var result = _repository.GetObjectBy(4);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Specific Task", result.Title);
        }

        [Fact]
        public void Update_Should_Modify_Task_In_Database()
        {
            // Arrange
            var task = new TaskManagement.Domain.Entities.Task { Id = 5, Title = "Old Title", ProjectId = 1 };
            _context.Set<TaskManagement.Domain.Entities.Task>().Add(task);
            _context.SaveChanges();

            // Act
            task.Title = "New Title";
            _repository.Update(task);
            _repository.Commit();

            // Assert
            var updated = _context.Set<TaskManagement.Domain.Entities.Task>().Find(5);
            Assert.NotNull(updated);
            Assert.Equal("New Title", updated.Title);
        }

        [Fact]
        public void Delete_Should_Remove_Task_From_Database()
        {
            // Arrange
            var task = new TaskManagement.Domain.Entities.Task { Id = 6, Title = "To Delete", ProjectId = 1 };
            _context.Set<TaskManagement.Domain.Entities.Task>().Add(task);
            _context.SaveChanges();

            // Act
            _repository.Delete(task);
            _repository.Commit();

            // Assert
            var result = _context.Set<TaskManagement.Domain.Entities.Task>().Find(6);
            Assert.Null(result);
        }

        [Fact]
        public void GetAverageTasksCompletedByUserOverLast30Days_Should_Return_Correct_Average()
        {
            // Arrange
            var userId1 = 1;
            var userId2 = 2;
            var now = DateTime.Now;

            var project1 = new Project { Id = 1, UserId = userId1, Title = "Project 1" };
            var project2 = new Project { Id = 2, UserId = userId2, Title = "Project 2" };

            _context.Set<Project>().AddRange(project1, project2);

            var tasks = new List<TaskManagement.Domain.Entities.Task>
            {
                new TaskManagement.Domain.Entities.Task { Id = 10, ProjectId = 1, Status = TaskManagement.Domain.Entities.Task.TaskStatus.Completed, UpdateAt = now.AddDays(-5), Title = "Task 1", Description = "Desc", Priority = TaskManagement.Domain.Entities.Task.TaskPriority.Low, DueDate = now.AddDays(1) },
                new TaskManagement.Domain.Entities.Task { Id = 11, ProjectId = 1, Status = TaskManagement.Domain.Entities.Task.TaskStatus.Completed, UpdateAt = now.AddDays(-10), Title = "Task 2", Description = "Desc", Priority = TaskManagement.Domain.Entities.Task.TaskPriority.Low, DueDate = now.AddDays(1) },
                new TaskManagement.Domain.Entities.Task { Id = 12, ProjectId = 2, Status = TaskManagement.Domain.Entities.Task.TaskStatus.Completed, UpdateAt = now.AddDays(-2), Title = "Task 3", Description = "Desc", Priority = TaskManagement.Domain.Entities.Task.TaskPriority.Low, DueDate = now.AddDays(1) }
            };

            _context.Set<TaskManagement.Domain.Entities.Task>().AddRange(tasks);
            _context.SaveChanges();

            // Act
            var average = _repository.GetAverageTasksCompletedByUserOverLast30Days();

            // Assert
            Assert.Equal(1.5m, average);
        }
    }
}
