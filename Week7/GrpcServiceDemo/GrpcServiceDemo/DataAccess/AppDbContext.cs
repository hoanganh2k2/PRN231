using Microsoft.EntityFrameworkCore;

namespace GrpcServiceDemo.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("GrpcDb"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee { employeeId = 1, firstName = "Hoang", lastName = "Anh" },
                new Employee { employeeId = 2, firstName = "Nguyen", lastName = "Hoang" },
                new Employee { employeeId = 3, firstName = "My", lastName = "Dung" },
                new Employee { employeeId = 4, firstName = "Duy", lastName = "Manh" });
        }
        public DbSet<Employee> Employees { get; set; }

    }
}
