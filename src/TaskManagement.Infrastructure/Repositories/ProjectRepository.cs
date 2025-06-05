using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Infrastructure.Context;

namespace TaskManagement.Infrastructure.Repositories
{
    public class ProjectRepository(DataBaseContext db) : BaseRepository<Project>(db), IProjectRepository
    {
        public List<Project> GetListBy(int userId)
        {
            return _db.Set<Project>().Where(x => x.UserId == userId).ToList();
        }
    }
}