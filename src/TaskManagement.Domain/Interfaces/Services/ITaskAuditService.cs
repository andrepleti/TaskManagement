using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Services
{
    public interface ITaskAuditService
    {
        List<TaskHistory> GenerateChanges(Entities.Task oldTask, Entities.Task newTask, int userId);
    }
}