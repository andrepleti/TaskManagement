using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Services
{
    public interface IProjectService
    {
        List<Project> GetListBy(int userId);
        void Add(Project task);

        void Delete(int id);
    }
}