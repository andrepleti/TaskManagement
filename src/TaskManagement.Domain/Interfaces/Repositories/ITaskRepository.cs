namespace TaskManagement.Domain.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        List<Entities.Task> GetListBy(int projectId);

        Entities.Task GetObjectBy(int id);

        void Add(Entities.Task task);

        void Delete(Entities.Task task);

        decimal GetAverageTasksCompletedByUserOverLast30Days();

        void Commit();
    }
}