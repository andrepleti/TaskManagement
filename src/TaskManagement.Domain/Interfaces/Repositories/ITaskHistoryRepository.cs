using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Repositories
{
    public interface ITaskHistoryRepository
    {
        List<TaskHistory> GetListBy(int taskId);

        void Add(TaskHistory task);

        void Commit();
    }
}