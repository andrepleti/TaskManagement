namespace TaskManagement.Domain.Entities
{
    public class Project(int id, int userId, string title)
    {
        public int Id { get; private set; } = id;
        public int UserId { get; private set; } = userId;
        public string Title { get; private set; } = title;
        public List<Task> Tasks { get; private set; } = [];
    }
}