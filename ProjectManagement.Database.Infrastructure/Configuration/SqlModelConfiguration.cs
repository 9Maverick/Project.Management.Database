using Microsoft.EntityFrameworkCore;
using ProjectManagement.Database.Shared.Kernel.Configuration;

namespace ProjectManagement.Database.Infrastructure.Configuration;

public class SqlModelConfiguration : IModelConfiguration
{
	public void ConfigureModel(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlModelConfiguration).Assembly);
	}
}