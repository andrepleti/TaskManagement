using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Domain.Interfaces.Services;

namespace TaskManagement.Application.Services
{
    public class ProjectService(IProjectRepository repository, ITaskRepository taskRepository) : IProjectService
    {
        IProjectRepository _repository = repository;
        ITaskRepository _taskRepository = taskRepository;

        public List<Project> GetListBy(int userId)
        {
            try
            {
                return _repository.GetListBy(userId);
            }
            catch
            {
                throw new Exception($"Erro ao buscar lista de projetos para o usuário {userId}.");
            }
        }

        public void Add(Project project)
        {
            try
            {
                _repository.Add(project);

                _repository.Commit();
            }
            catch
            {
                throw new Exception("Erro ao cadastrar.");
            }
        }

        public void Delete(int id)
        {
            if (CheckIfExistsPendingTasksBy(id))
                throw new Exception(@"Não foi possível deletar o projeto, o mesmo possui tarefas pendentes.\n
                                        Conclua ou remova as tarefas pendentes.");

            var project = _repository.GetObjectBy(id);

            _repository.Delete(project);

            _repository.Commit();
        }

        private bool CheckIfExistsPendingTasksBy(int projectId)
        {
            var tasks = _taskRepository.GetListBy(projectId);

            return tasks.Where(x => x.Status == Domain.Entities.Task.TaskStatus.Pending).Any();
        }
    }
}