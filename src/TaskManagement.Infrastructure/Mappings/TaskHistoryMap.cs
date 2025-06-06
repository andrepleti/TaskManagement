using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Mappings
{
    [ExcludeFromCodeCoverage]
    public class TaskHistoryMap
    {
        public void Configure(EntityTypeBuilder<TaskHistory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ChangedField)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.OldValue)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.NewValue)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.ChangedAt)
                .IsRequired();

            builder.Property(x => x.ChangedByUserId)
                .IsRequired();

            builder.Property(x => x.Comment)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(t => t.CreateAt)
                .IsRequired();

            builder.Property(t => t.UpdateAt)
                .IsRequired();

            builder.ToTable(nameof(TaskHistory));
        }
    }
}