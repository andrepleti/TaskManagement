using TaskManagement.Application.Services;

namespace TaskManagement.Tests.Unit.Application
{
    public class TaskAuditServiceTests
    {
        private readonly TaskAuditService _service = new TaskAuditService();

        [Fact]
        public void GenerateChanges_Should_Return_ChangedFields()
        {
            // Arrange
            var oldTask = new TaskManagement.Domain.Entities.Task
            {
                Id = 1,
                Title = "Old",
                Description = "Old desc",
                Status = TaskManagement.Domain.Entities.Task.TaskStatus.Pending,
                DueDate = new DateTime(2024, 1, 1)
            };
            var newTask = new TaskManagement.Domain.Entities.Task
            {
                Id = 1,
                Title = "New",
                Description = "New desc",
                Status = TaskManagement.Domain.Entities.Task.TaskStatus.Completed,
                DueDate = new DateTime(2024, 2, 1)
            };
            int userId = 99;

            // Act
            var changes = _service.GenerateChanges(oldTask, newTask, userId);

            // Assert
            Assert.Equal(4, changes.Count);
            Assert.Contains(changes, c => c.ChangedField == "Title" && c.OldValue == "Old" && c.NewValue == "New");
            Assert.Contains(changes, c => c.ChangedField == "Description" && c.OldValue == "Old desc" && c.NewValue == "New desc");
            Assert.Contains(changes, c => c.ChangedField == "Status" && c.OldValue == "Pending" && c.NewValue == "Completed");
            Assert.Contains(changes, c => c.ChangedField == "DueDate" && c.OldValue == "2024-01-01T00:00:00" && c.NewValue == "2024-02-01T00:00:00");
            Assert.All(changes, c => Assert.Equal(userId, c.ChangedByUserId));
        }
    }

}
