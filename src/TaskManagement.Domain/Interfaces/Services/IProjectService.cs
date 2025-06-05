using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Services
{
    public interface IProjectService: IBaseService<Project>
    {
        List<Project> GetListBy(int userId);
    }
}