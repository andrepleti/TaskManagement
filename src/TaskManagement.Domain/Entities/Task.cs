namespace TaskManagement.Domain.Entities
{
    public class Task(int id, int projectId, string title, string description, DateTime dueDate, Task.TaskStatus status, Task.TaskPriority priority)
    {
        public int Id { get; set; } = id;
        public int ProjectId { get; set; } = projectId;
        public string Title { get; set; } = title;
        public string Description { get; set; } = description;
        public DateTime DueDate { get; set; } = dueDate;
        public TaskStatus Status { get; set; } = status;
        public TaskPriority Priority { get; set; } = priority;
        public Project Project { get; set; } = new Project(0, 0, string.Empty);

        public enum TaskStatus
        {
            Pending = 1,
            InProgress = 2,
            Completed = 3
        }

        public enum TaskPriority
        {
            Low = 1,
            Medium = 2,
            High = 3
        }
    }
}