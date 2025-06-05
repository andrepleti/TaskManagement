namespace TaskManagement.Domain.Interfaces.Repositories
{
    public interface ITaskRepository : IBaseRepository<Entities.Task>
    {
        List<Entities.Task> GetListBy(int projectId);

        void Update(Entities.Task task);

        
    }
}