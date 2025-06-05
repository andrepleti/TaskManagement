using Moq;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Tests.Unit.Application
{
    public class ProjectServiceTests
    {
        private readonly Mock<IProjectRepository> _mockProjectRepository;
        private readonly Mock<ITaskRepository> _mockTaskRepository;
        private readonly ProjectService _service;

        public ProjectServiceTests()
        {
            _mockProjectRepository = new Mock<IProjectRepository>();
            _mockTaskRepository = new Mock<ITaskRepository>();
            _service = new ProjectService(_mockProjectRepository.Object, _mockTaskRepository.Object);
        }

        [Fact]
        public void GetListBy_Should_Return_Projects()
        {
            // Arrange
            var userId = 1;
            var projects = new List<Project> { new Project { Id = 1, UserId = userId, Title = "Proj 1" } };
            _mockProjectRepository.Setup(x => x.GetListBy(userId)).Returns(projects);

            // Act
            var result = _service.GetListBy(userId);

            // Assert
            Assert.Single(result);
            Assert.Equal("Proj 1", result.First().Title);
        }

        [Fact]
        public void Add_Should_Call_Add_And_Commit()
        {
            // Arrange
            var project = new Project { Id = 1 };

            // Act
            _service.Add(project);

            // Assert
            _mockProjectRepository.Verify(x => x.Add(project), Times.Once);
            _mockProjectRepository.Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public void Delete_Should_Throw_If_PendingTasksExist()
        {
            // Arrange
            int projectId = 1;
            _mockTaskRepository.Setup(x => x.GetListBy(projectId))
                .Returns(new List<TaskManagement.Domain.Entities.Task> { new TaskManagement.Domain.Entities.Task { Status = TaskManagement.Domain.Entities.Task.TaskStatus.Pending } });

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _service.Delete(projectId));
            Assert.Contains("tarefas pendentes", ex.Message);
        }

        [Fact]
        public void Delete_Should_Call_Delete_And_Commit_If_NoPendingTasks()
        {
            // Arrange
            int projectId = 1;
            _mockTaskRepository.Setup(x => x.GetListBy(projectId))
                .Returns(new List<TaskManagement.Domain.Entities.Task>());
            var project = new Project { Id = projectId };
            _mockProjectRepository.Setup(x => x.GetObjectBy(projectId)).Returns(project);

            // Act
            _service.Delete(projectId);

            // Assert
            _mockProjectRepository.Verify(x => x.Delete(project), Times.Once);
            _mockProjectRepository.Verify(x => x.Commit(), Times.Once);
        }
    }
}
