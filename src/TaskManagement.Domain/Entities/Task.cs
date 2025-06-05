namespace TaskManagement.Domain.Entities
{
    public class Task : BaseEntity
    {
        public Task()
        {
            this.ProjectId = 0;
            this.Description = string.Empty;
            this.DueDate = new DateTime();
            this.Status = TaskStatus.Pending;
            this.Priority = TaskPriority.Low;
            this.Project = new Project();
        }

        public int ProjectId { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public Project Project { get; set; }

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