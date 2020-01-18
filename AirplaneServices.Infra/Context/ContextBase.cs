using AirplaneServices.Domain.Entities;
using AirplaneServices.Infra.Map;
using Microsoft.EntityFrameworkCore;

namespace AirplaneServices.Infra.Context
{
    public class ContextBase : DbContext
    {

        public ContextBase() { }

        public ContextBase(DbContextOptions<ContextBase> options) : base(options) { }

        public DbSet<AirPlaneModel> AirPlaneModel { get; set; }

        public DbSet<AirPlane> AirPlane { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AirPlaneModelConfigurations());
            modelBuilder.ApplyConfiguration(new AirPlaneConfigurations());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                   .UseSqlServer(this.StringConnectionConfig()); //.UseLazyLoadingProxies()
            }
        }

        private string StringConnectionConfig()
        {
            var conn = @"Data Source=DESKTOP-OUM5KHF\SQLEXPRESS;Initial Catalog=Airplane;Integrated Security=True";
            return conn;
        }
    }

}
