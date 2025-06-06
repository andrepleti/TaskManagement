using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Infrastructure.Context;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskRepository(DataBaseContext _db) : ITaskRepository
    {
        public List<Domain.Entities.Task> GetListBy(int projectId)
        {
            return _db.Set<Domain.Entities.Task>().Where(x => x.ProjectId == projectId).ToList();
        }

        public Domain.Entities.Task GetObjectBy(int id)
        {
            return _db.Set<Domain.Entities.Task>().Where(x => x.Id == id).FirstOrDefault()!;
        }

        public void Add(Domain.Entities.Task task)
        {
            _db.Entry(task).State = EntityState.Added;
        }
        
        public void Delete(Domain.Entities.Task task)
        {
            _db.Remove(task);
        }

        public void Commit()
        {
            _db.SaveChanges();
        }

        public decimal GetAverageTasksCompletedByUserOverLast30Days()
        {
            var tasks = _db.Set<Domain.Entities.Task>()
                            .Where(x => x.Status == Domain.Entities.Task.TaskStatus.Completed &&
                                        x.UpdateAt >= DateTime.Now.AddDays(-30))
                            .Include(x => x.Project)
                            .ToList();

            return tasks.Select(x => x.Id).Distinct().Count() / tasks.Select(x => x.Project.UserId).Distinct().Count();

        }
    }
}