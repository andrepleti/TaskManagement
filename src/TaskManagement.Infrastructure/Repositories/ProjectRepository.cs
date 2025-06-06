using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Infrastructure.Context;

namespace TaskManagement.Infrastructure.Repositories
{
    public class ProjectRepository(DataBaseContext _db) : IProjectRepository
    {
        public List<Project> GetListBy(int userId)
        {
            return _db.Set<Project>().Where(x => x.UserId == userId).ToList();
        }

        public Project GetObjectBy(int id)
        {
            return _db.Set<Project>().FirstOrDefault(x => x.Id == id)!;
        }

        public void Add(Project project)
        {
            _db.Entry(project).State = EntityState.Added;
        }
        
        public void Delete(Project project)
        {
            _db.Entry(project).State = EntityState.Deleted;
        }

        public void Commit()
        {
            _db.SaveChanges();
        }
    }
}