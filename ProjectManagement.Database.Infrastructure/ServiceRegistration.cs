using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Database.Data;
using ProjectManagement.Database.Infrastructure.Configuration;
using ProjectManagement.Database.Shared.Kernel.Configuration;
using System.Diagnostics;

namespace ProjectManagement.Database.Infrastructure;

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