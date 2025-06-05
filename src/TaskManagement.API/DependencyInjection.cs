using TaskManagement.Application.Services;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Domain.Interfaces.Services;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagement.API
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectService, ProjectService>();

            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddScoped<ITaskHistoryRepository, TaskHistoryRepository>();
            services.AddScoped<ITaskAuditService, TaskAuditService>();
            
        }
    }
}