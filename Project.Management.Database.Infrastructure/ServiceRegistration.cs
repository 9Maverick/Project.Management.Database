using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Management.Database.Data;
using Project.Management.Database.Infrastructure.Configuration;
using Project.Management.Database.Shared.Kernel.Configuration;
using System.Diagnostics;

namespace Project.Management.Database.Infrastructure;

public static class ServiceRegistration
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString, bool isDevelopment)
	{
		services.AddSingleton<IModelConfiguration, SqlModelConfiguration>();
		services.AddDbContext<DatabaseContext>(options =>
		{
			options.UseSqlServer(connectionString, sqlOptions =>
			{
				sqlOptions.MigrationsAssembly(
					typeof(ServiceRegistration).Assembly.FullName);
			});

			if (isDevelopment)
			{
				options.EnableSensitiveDataLogging();
			}

			options.LogTo(message => Trace.WriteLine(message));
		});

		return services;
	}
}