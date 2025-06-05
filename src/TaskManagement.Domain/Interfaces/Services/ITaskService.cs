namespace TaskManagement.Domain.Interfaces.Services
{
    public interface ITaskService
    {
        List<Entities.Task> GetListBy(int projectId);

        void Add(Entities.Task task);

        void Update(int userId, Entities.Task task);

        void Delete(int id);
        
        decimal GetAverageTasksCompletedByUserOverLast30Days();
    }
}