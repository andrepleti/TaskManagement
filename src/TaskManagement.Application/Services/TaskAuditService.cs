using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Services;

namespace TaskManagement.Application.Services
{
    public class TaskAuditService : ITaskAuditService
    {
        public List<TaskHistory> GenerateChanges(Domain.Entities.Task oldTask, Domain.Entities.Task newTask, int userId)
        {
            var history = new List<TaskHistory>();

            if (oldTask.Title != newTask.Title)
                history.Add(Create("Title", oldTask.Title, newTask.Title, newTask.Id, userId));

            if (oldTask.Description != newTask.Description)
                history.Add(Create("Description", oldTask.Description, newTask.Description, newTask.Id, userId));

            if (oldTask.Status != newTask.Status)
                history.Add(Create("Status", oldTask.Status.ToString(), newTask.Status.ToString(), newTask.Id, userId));

            if (oldTask.DueDate != newTask.DueDate)
                history.Add(Create("DueDate", oldTask.DueDate.ToString("s"), newTask.DueDate.ToString("s"), newTask.Id, userId));

            return history;
        }

        private static TaskHistory Create(string field, string oldValue, string newValue, int taskId, int userId)
        {
            return new TaskHistory
            {
                TaskId = taskId,
                ChangedField = field,
                OldValue = oldValue,
                NewValue = newValue,
                ChangedAt = DateTime.UtcNow,
                ChangedByUserId = userId
            };
        }
    }
}