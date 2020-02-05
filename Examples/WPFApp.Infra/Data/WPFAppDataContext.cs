using Microsoft.EntityFrameworkCore;
using WPFApp.Infra.Mappings;

namespace WPFApp.Infra.Data
{
    public class WPFAppDataContext : DbContext
    {
        public WPFAppDataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskMap());
            modelBuilder.ApplyConfiguration(new TaskLogMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
