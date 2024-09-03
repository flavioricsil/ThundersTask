using Microsoft.EntityFrameworkCore;
using ThundersTasks.Infrastructure.Data.Configurations;
using TaskModel = ThundersTasks.Core.Models.TaskModel;

namespace ThundersTasks.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public DbSet<TaskModel> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TaskConfiguration());
        }
    }
}

