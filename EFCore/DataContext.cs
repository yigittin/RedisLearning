using EFCore.DbModels;
using Microsoft.EntityFrameworkCore;

namespace EFCore
{
    //dotnet ef migrations add InitialMigration -s ..\RedisLearning\RedisLearning.csproj
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Driver> Drivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            for(int i = 1; i <= 10000; i++)
            {
                Driver driver = new Driver()
                {
                    DriverNumber = i + 10000,
                    Id = i,
                    Name = DumbData.CreateName()
                };
                modelBuilder.Entity<Driver>().HasData(
                    driver
                    );
            }
        }
    }
}