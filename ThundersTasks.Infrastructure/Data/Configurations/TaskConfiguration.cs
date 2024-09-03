using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThundersTasks.Core.Enums;
using TaskModel = ThundersTasks.Core.Models.TaskModel;

namespace ThundersTasks.Infrastructure.Data.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<TaskModel>
    {
        public void Configure(EntityTypeBuilder<TaskModel> builder)
        {
            builder.ToTable("Task");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();

            builder.Property(p => p.Title).HasColumnName("Title").HasMaxLength(100).IsRequired();

            builder.Property(p => p.Status)
       .HasConversion(
            v => v.ToString(),
            v => (EnumTaskStatus)System.Enum.Parse(typeof(EnumTaskStatus), v));
        }
    }
}
