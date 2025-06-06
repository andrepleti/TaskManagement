using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Domain.Interfaces.Services;
using static TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Application.Services
{
    public class TaskService(ITaskRepository _repository, ITaskHistoryRepository _taskHistoryRepository, ITaskAuditService _auditService) : ITaskService
    {
        public List<Domain.Entities.Task> GetListBy(int projectId)
        {
            try
            {
                return _repository.GetListBy(projectId);
            }
            catch
            {
                throw new Exception($"Erro ao buscar lista de tarefas para o projeto {projectId}.");
            }
        }

        public void Add(Domain.Entities.Task task)
        {
            if (CheckIfProjectHas20TasksBy(task.ProjectId))
                throw new Exception($"Projeto já possui 20 tarefas.");

            _repository.Add(task);

            _repository.Commit();
        }

        public void Update(int userId, Domain.Entities.Task task)
        {
            try
            {
                var existingTask = RecoverOriginalTask(task.Id);

                task.Priority = RecoverOriginalTaskPriority(existingTask);

                var oldTask = Clone(existingTask);

                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.DueDate = task.DueDate;
                existingTask.Status = task.Status;
                existingTask.Priority = task.Priority;
                existingTask.Comment = task.Comment;

                CreateHistory(oldTask, task, userId);

                _repository.Commit();
            }
            catch
            {
                throw new Exception("Erro ao atualizar tarefa.");
            }
        }

        public void Delete(int id)
        {
            try
            {
                var task = _repository.GetObjectBy(id);

                _repository.Delete(task);

                _repository.Commit();
            }
            catch
            {
                throw new Exception("Erro ao deletar tarefa.");
            }
        }

        public decimal GetAverageTasksCompletedByUserOverLast30Days()
        {
            try
            {
                return _repository.GetAverageTasksCompletedByUserOverLast30Days();
            }
            catch
            {
                throw new Exception("Erro ao carregar o relatório de tarefas concluídas.");
            }
        }

        private Domain.Entities.Task RecoverOriginalTask(int id)
        {
            return _repository.GetObjectBy(id);
        }

        private TaskPriority RecoverOriginalTaskPriority(Domain.Entities.Task task)
        {
            return task.Priority;
        }

        private bool CheckIfProjectHas20TasksBy(int projectId)
        {
            var tasks = _repository.GetListBy(projectId);

            return tasks.Count() >= 20;
        }

        private void CreateHistory(Domain.Entities.Task oldTask, Domain.Entities.Task newTask, int userId)
        {
            var changes = _auditService.GenerateChanges(oldTask, newTask, userId);

            foreach (var history in changes)
            {
                history.Comment = newTask.Comment;
                _taskHistoryRepository.Add(history);
            }
        }

        private Domain.Entities.Task Clone(Domain.Entities.Task task)
        {
            return new Domain.Entities.Task
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status,
                Priority = task.Priority,
                Comment = task.Comment
            };
        }
    }
}