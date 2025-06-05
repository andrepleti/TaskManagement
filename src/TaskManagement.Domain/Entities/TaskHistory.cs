namespace TaskManagement.Domain.Entities
{
    public class TaskHistory : BaseEntity
    {
        public TaskHistory()
        {
            this.TaskId = 0;
            this.ChangedField = string.Empty;
            this.OldValue = string.Empty;
            this.NewValue = string.Empty;
            this.ChangedAt = new DateTime();
            this.ChangedByUserId = 0;
            this.Comment = string.Empty;
            this.Task = new Task();
        }

        public int TaskId { get; set; }
        public string ChangedField { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime ChangedAt { get; set; }
        public int ChangedByUserId { get; set; }
        public string Comment { get; set; }
        public Task Task { get; set; }
    }
}