using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WPFApp.Domain.Entities;

namespace WPFApp.Infra.Mappings
{
    public class TaskMap : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {

            builder.HasKey(tsk => tsk.Id);
            
            builder.Property(tsk => tsk.Deadline)
                .IsRequired();

            builder.Property(tsk => tsk.Name)
                .HasMaxLength(75)
                .IsRequired();

            builder.Property(tsk => tsk.Description)
                .HasMaxLength(1000);

            builder.HasMany(tsk => tsk.Logs)
                .WithOne(log => log.Task)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
