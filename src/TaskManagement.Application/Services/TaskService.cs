using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Domain.Interfaces.Services;
using static TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Application.Services
{
    public class TaskService(ITaskRepository _repository) :  ITaskService
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
            try
            {
                if (CheckIfProjectHas20TasksBy(task.ProjectId))
                throw new Exception($"Projeto já possui 20 tarefas.");

                _repository.Add(task);

                _repository.Commit();
            }
            catch
            {
                throw new Exception("Erro ao cadastrar tarefa.");
            }
        }

        public void Update(Domain.Entities.Task task)
        {
            try
            {
                task.Priority = RecoverOriginalTaskPriority(task.Id);

                _repository.Update(task);

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

        private TaskPriority RecoverOriginalTaskPriority(int id)
        {
            return _repository.GetObjectBy(id).Priority;
        }

        private bool CheckIfProjectHas20TasksBy(int projectId)
        {
            var tasks = _repository.GetListBy(projectId);

            return tasks.Count() >= 20;
        }
    }
}