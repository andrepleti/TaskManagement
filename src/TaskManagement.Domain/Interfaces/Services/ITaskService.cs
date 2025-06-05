namespace TaskManagement.Domain.Interfaces.Services
{
    public interface ITaskService: IBaseService<Entities.Task>
    {
        List<Entities.Task> GetListBy(int projectId);
        
        void Update(Entities.Task task);
    }
}