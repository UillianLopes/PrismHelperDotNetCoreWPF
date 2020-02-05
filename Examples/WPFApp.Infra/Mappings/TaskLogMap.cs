using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WPFApp.Domain.Entities;
using WPFApp.Domain.Enums;

namespace WPFApp.Infra.Mappings
{
    public class TaskLogMap : IEntityTypeConfiguration<TaskLog>
    {
        public void Configure(EntityTypeBuilder<TaskLog> builder)
        {
            builder.HasKey(log => log.Id);
            
            builder.Property(log => log.Date)
                .IsRequired();

            builder.Property(log => log.Message)
                .HasMaxLength(1000);

            builder.Property(log => log.State)
                .HasConversion((log) => log.ToString(), log => (TaskState)Enum.Parse(typeof(TaskState), log))
                .IsRequired();

            builder.HasOne(log => log.Task)
                .WithMany(tsk => tsk.Logs)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
