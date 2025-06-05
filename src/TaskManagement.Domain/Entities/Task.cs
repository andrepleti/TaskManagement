namespace TaskManagement.Domain.Entities
{
    public class Task : BaseEntity
    {
        public Task()
        {
            this.ProjectId = 0;
            this.Title = string.Empty;
            this.Description = string.Empty;
            this.DueDate = new DateTime();
            this.Status = TaskStatus.Pending;
            this.Priority = TaskPriority.Low;
            this.Comment = string.Empty;
            this.Project = new Project();
            this.TaskHistories = [];
        }

        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public string Comment { get; set; }
        public Project Project { get; set; }
        public List<TaskHistory> TaskHistories { get; set; }

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