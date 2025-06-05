using TaskManagement.Domain.Entities;

namespace TaskManagement.Tests.Unit.Domain
{
    public class TaskTests
    {
        [Fact]
        public void Constructor_Should_Initialize_Properties()
        {
            // Act
            var task = new TaskManagement.Domain.Entities.Task();

            // Assert
            Assert.Equal(0, task.ProjectId);
            Assert.Equal(string.Empty, task.Title);
            Assert.Equal(string.Empty, task.Description);
            Assert.Equal(default, task.DueDate);
            Assert.Equal(TaskManagement.Domain.Entities.Task.TaskStatus.Pending, task.Status);
            Assert.Equal(TaskManagement.Domain.Entities.Task.TaskPriority.Low, task.Priority);
            Assert.Equal(string.Empty, task.Comment);
            Assert.NotNull(task.Project);
            Assert.NotNull(task.TaskHistories);
            Assert.IsType<List<TaskHistory>>(task.TaskHistories);
            Assert.Empty(task.TaskHistories);
        }

        [Fact]
        public void TaskStatus_Enum_Should_Have_Correct_Values()
        {
            Assert.Equal(1, (int)TaskManagement.Domain.Entities.Task.TaskStatus.Pending);
            Assert.Equal(2, (int)TaskManagement.Domain.Entities.Task.TaskStatus.InProgress);
            Assert.Equal(3, (int)TaskManagement.Domain.Entities.Task.TaskStatus.Completed);
        }

        [Fact]
        public void TaskPriority_Enum_Should_Have_Correct_Values()
        {
            Assert.Equal(1, (int)TaskManagement.Domain.Entities.Task.TaskPriority.Low);
            Assert.Equal(2, (int)TaskManagement.Domain.Entities.Task.TaskPriority.Medium);
            Assert.Equal(3, (int)TaskManagement.Domain.Entities.Task.TaskPriority.High);
        }
    }
}
