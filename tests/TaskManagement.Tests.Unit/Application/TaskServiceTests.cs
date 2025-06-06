using Moq;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Domain.Interfaces.Services;

namespace TaskManagement.Tests.Unit.Application
{

    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _mockTaskRepository;
        private readonly Mock<ITaskHistoryRepository> _mockTaskHistoryRepository;
        private readonly Mock<ITaskAuditService> _mockAuditService;
        private readonly TaskService _service;

        public TaskServiceTests()
        {
            _mockTaskRepository = new Mock<ITaskRepository>();
            _mockTaskHistoryRepository = new Mock<ITaskHistoryRepository>();
            _mockAuditService = new Mock<ITaskAuditService>();
            _service = new TaskService(_mockTaskRepository.Object, _mockTaskHistoryRepository.Object, _mockAuditService.Object);
        }

        [Fact]
        public void GetListBy_Should_Return_Tasks()
        {
            // Arrange
            int projectId = 1;
            var tasks = new List<TaskManagement.Domain.Entities.Task> { new TaskManagement.Domain.Entities.Task { Id = 1, ProjectId = projectId } };
            _mockTaskRepository.Setup(x => x.GetListBy(projectId)).Returns(tasks);

            // Act
            var result = _service.GetListBy(projectId);

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result.First().Id);
        }

        [Fact]
        public void Add_Should_Throw_When_More_Than_20Tasks()
        {
            // Arrange
            int projectId = 1;
            var task = new TaskManagement.Domain.Entities.Task { ProjectId = projectId };
            var tasks = Enumerable.Range(1, 20).Select(i => new TaskManagement.Domain.Entities.Task { Id = i, ProjectId = projectId }).ToList();
            _mockTaskRepository.Setup(x => x.GetListBy(projectId)).Returns(tasks);

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _service.Add(task));
            Assert.Contains("já possui 20 tarefas", ex.Message);
        }

        [Fact]
        public void Add_Should_Call_Add_And_Commit()
        {
            // Arrange
            int projectId = 1;
            var task = new TaskManagement.Domain.Entities.Task { ProjectId = projectId };
            _mockTaskRepository.Setup(x => x.GetListBy(projectId)).Returns(new List<TaskManagement.Domain.Entities.Task>());

            // Act
            _service.Add(task);

            // Assert
            _mockTaskRepository.Verify(x => x.Add(task), Times.Once);
            _mockTaskRepository.Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public void Update_Should_Update_Task_And_Add_History()
        {
            // Arrange
            int userId = 1;

            var oldTask = new TaskManagement.Domain.Entities.Task
            {
                Id = 1,
                Title = "Old Title",
                Description = "Old Description",
                DueDate = DateTime.Today,
                Status = TaskManagement.Domain.Entities.Task.TaskStatus.Pending,
                Priority = TaskManagement.Domain.Entities.Task.TaskPriority.Medium,
                Comment = "Old Comment"
            };

            var newTask = new TaskManagement.Domain.Entities.Task
            {
                Id = 1,
                Title = "New Title",
                Description = "New Description",
                DueDate = DateTime.Today.AddDays(1),
                Status = TaskManagement.Domain.Entities.Task.TaskStatus.InProgress,
                Priority = TaskManagement.Domain.Entities.Task.TaskPriority.Low, // será sobrescrito
                Comment = "New Comment"
            };

            var expectedHistory = new List<TaskHistory>
            {
                new TaskHistory
                {
                    TaskId = 1,
                    ChangedField = "Title",
                    OldValue = oldTask.Title,
                    NewValue = newTask.Title
                },
                new TaskHistory
                {
                    TaskId = 1,
                    ChangedField = "Description",
                    OldValue = oldTask.Description,
                    NewValue = newTask.Description
                },
                // pode adicionar outros campos conforme lógica da CreateHistory
            };

            _mockTaskRepository.Setup(x => x.GetObjectBy(newTask.Id)).Returns(oldTask);
            _mockAuditService.Setup(x => x.GenerateChanges(It.IsAny<TaskManagement.Domain.Entities.Task>(), It.IsAny<TaskManagement.Domain.Entities.Task>(), userId))
                            .Returns(expectedHistory);

            // Act
            _service.Update(userId, newTask);

            // Assert
            _mockTaskRepository.Verify(x => x.GetObjectBy(newTask.Id), Times.Once);
            _mockTaskHistoryRepository.Verify(x => x.Add(It.IsAny<TaskHistory>()), Times.Exactly(expectedHistory.Count));
            _mockTaskRepository.Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public void Delete_Should_Call_Delete_And_Commit()
        {
            // Arrange
            int id = 1;
            var task = new TaskManagement.Domain.Entities.Task { Id = id };
            _mockTaskRepository.Setup(x => x.GetObjectBy(id)).Returns(task);

            // Act
            _service.Delete(id);

            // Assert
            _mockTaskRepository.Verify(x => x.Delete(task), Times.Once);
            _mockTaskRepository.Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public void GetAverageTasksCompletedByUserOverLast30Days_Should_Return_Correct_Value()
        {
            // Arrange
            decimal expected = 2.5m;
            _mockTaskRepository.Setup(x => x.GetAverageTasksCompletedByUserOverLast30Days()).Returns(expected);

            // Act
            var result = _service.GetAverageTasksCompletedByUserOverLast30Days();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}