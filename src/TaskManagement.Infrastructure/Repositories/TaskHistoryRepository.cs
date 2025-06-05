using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Infrastructure.Context;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskHistoryRepository(DataBaseContext _db) : ITaskHistoryRepository
    {
        public List<TaskHistory> GetListBy(int taskId)
        {
            return _db.Set<TaskHistory>().Where(x => x.TaskId == taskId).ToList();
        }

        public void Add(TaskHistory entity)
        {
            _db.Entry(entity).State = EntityState.Added;
        }

        public void Commit()
        {
            _db.SaveChanges();
        }
    }
}