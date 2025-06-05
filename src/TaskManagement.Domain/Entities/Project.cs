namespace TaskManagement.Domain.Entities
{
    public class Project : BaseEntity
    {
        public Project()
        {
            this.UserId = 0;
            this.Tasks = [];
        }

        public int UserId { get; set; }
        public List<Task> Tasks { get; set; }
    }
}