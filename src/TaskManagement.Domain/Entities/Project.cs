namespace TaskManagement.Domain.Entities
{
    public class Project : BaseEntity
    {
        public Project()
        {
            this.UserId = 0;
            this.Title = string.Empty;
            this.Tasks = [];
        }

        public int UserId { get; set; }
        public string Title { get; set; }
        public List<Task> Tasks { get; set; }
    }
}