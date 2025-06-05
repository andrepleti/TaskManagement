using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Repositories
{

    public interface IProjectRepository
    {
        List<Project> GetListBy(int userId);
        
        Project GetObjectBy(int id);

        void Add(Project project);

        void Delete(Project project);

        void Commit();
    }
}