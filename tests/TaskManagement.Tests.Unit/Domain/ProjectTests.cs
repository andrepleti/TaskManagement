using TaskManagement.Domain.Entities;

namespace TaskManagement.Tests.Unit.Domain
{
    public class ProjectTests
    {
        [Fact]
        public void Constructor_Should_Initialize_Properties()
        {
            // Act
            var project = new Project();

            // Assert
            Assert.Equal(0, project.UserId);
            Assert.Equal(string.Empty, project.Title);
            Assert.NotNull(project.Tasks);
            Assert.IsType<List<TaskManagement.Domain.Entities.Task>>(project.Tasks);
            Assert.Empty(project.Tasks);
        }
    }
}
