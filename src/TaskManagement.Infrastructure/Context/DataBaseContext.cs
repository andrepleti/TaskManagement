using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Mapping;

namespace TaskManagement.Infrastructure.Context
{
    public class DataBaseContext(DbContextOptions<DataBaseContext> options) : DbContext(options)
    {

        public DbSet<Project> Projects { get; set; }
        public DbSet<Domain.Entities.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>(new ProjectMap().Configure);
            modelBuilder.Entity<Domain.Entities.Task>(new TaskMap().Configure);
        }
    }
}