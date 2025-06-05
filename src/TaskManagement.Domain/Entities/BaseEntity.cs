namespace TaskManagement.Domain.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            this.Id = 0;
            this.CreateAt = new DateTime();
            this.UpdateAt = new DateTime();
        }

        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}