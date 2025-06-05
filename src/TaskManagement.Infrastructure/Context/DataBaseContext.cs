using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Mappings;

namespace TaskManagement.Infrastructure.Context
{
    public class DataBaseContext(DbContextOptions<DataBaseContext> options) : DbContext(options)
    {

        public DbSet<Project> Projects { get; set; }
        public DbSet<Domain.Entities.Task> Tasks { get; set; }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow; // data e hora atual

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreateAt = now;
                }
                ((BaseEntity)entity.Entity).UpdateAt = now;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>(new ProjectMap().Configure);
            modelBuilder.Entity<Domain.Entities.Task>(new TaskMap().Configure);
        }
    }
}