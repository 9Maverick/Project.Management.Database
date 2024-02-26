using Microsoft.EntityFrameworkCore;

namespace Project.Management.Database.Shared.Kernel.Configuration;

public interface IModelConfiguration
{
	void ConfigureModel(ModelBuilder modelBuilder);
}
