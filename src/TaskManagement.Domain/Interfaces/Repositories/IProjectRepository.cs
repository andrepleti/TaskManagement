using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Repositories
{

    public interface IProjectRepository: IBaseRepository<Project>
    {
        List<Project> GetListBy(int userId);
    }
}