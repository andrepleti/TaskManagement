using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Infrastructure.Context;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskRepository(DataBaseContext db) : BaseRepository<Domain.Entities.Task>(db), ITaskRepository
    {
        public List<Domain.Entities.Task> GetListBy(int projectId)
        {
            return _db.Set<Domain.Entities.Task>().Where(x => x.ProjectId == projectId).ToList();
        }

        public void Update(Domain.Entities.Task task)
        {
            _db.Entry(task).State = EntityState.Modified;
        }
    }
}