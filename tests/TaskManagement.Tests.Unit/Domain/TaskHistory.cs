using TaskManagement.Domain.Entities;

namespace TaskManagement.Tests.Unit.Domain
{
    public class TaskHistoryTests
    {
        [Fact]
        public void Constructor_Should_Initialize_Properties()
        {
            // Act
            var history = new TaskHistory();

            // Assert
            Assert.Equal(0, history.TaskId);
            Assert.Equal(string.Empty, history.ChangedField);
            Assert.Equal(string.Empty, history.OldValue);
            Assert.Equal(string.Empty, history.NewValue);
            Assert.Equal(default, history.ChangedAt);
            Assert.Equal(0, history.ChangedByUserId);
            Assert.Equal(string.Empty, history.Comment);
            Assert.NotNull(history.Task);
        }
    }
}
