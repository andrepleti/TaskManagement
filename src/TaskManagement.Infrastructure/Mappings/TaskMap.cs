using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManagement.Infrastructure.Mappings
{
    [ExcludeFromCodeCoverage]
    public class TaskMap
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.DueDate)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.Priority)
                .IsRequired();

            builder.Property(x => x.Comment)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(t => t.CreateAt)
                .IsRequired();

            builder.Property(t => t.UpdateAt)
                .IsRequired();

            builder.HasMany(x => x.TaskHistories)
                .WithOne(x => x.Task)
                .HasForeignKey(x => x.TaskId)
                .HasPrincipalKey(x => x.Id);

            builder.ToTable(nameof(Domain.Entities.Task));
        }
    }
}