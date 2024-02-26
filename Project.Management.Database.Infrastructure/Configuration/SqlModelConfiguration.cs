using Microsoft.EntityFrameworkCore;
using Project.Management.Database.Shared.Kernel.Configuration;

namespace Project.Management.Database.Infrastructure.Configuration;

public class SqlModelConfiguration : IModelConfiguration
{
	public void ConfigureModel(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlModelConfiguration).Assembly);
	}
}