using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Project.Management.Database.Data;
using Project.Management.Database.Infrastructure;
using Project.Management.Database.Infrastructure.Configuration;

namespace Project.Management.Database.API.DesignTime;

public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
	private const string AdminConnectionString = "PROJECT_MANAGEMENT_ADMIN_CONNECTION_STRING";
	public DatabaseContext CreateDbContext(string[] args)
	{
		Environment.SetEnvironmentVariable(AdminConnectionString, "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog=ProjectManagement;Integrated Security=true;Trusted_Connection=True");
		var connectionString = Environment.GetEnvironmentVariable(AdminConnectionString);
		if (string.IsNullOrEmpty(connectionString))
		{
			throw new ApplicationException(
				$"Error, absent {AdminConnectionString}");
		}
		var options = new DbContextOptionsBuilder<DatabaseContext>()
			.UseSqlServer(connectionString, sqlOptions =>
			{
				sqlOptions.MigrationsAssembly(
					typeof(ServiceRegistration).Assembly.FullName);
			})
			.Options;
		return new DatabaseContext(options, new SqlModelConfiguration());
	}
}
