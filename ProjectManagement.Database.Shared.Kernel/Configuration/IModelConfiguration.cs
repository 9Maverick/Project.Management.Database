using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Database.Shared.Kernel.Configuration;

public interface IModelConfiguration
{
	void ConfigureModel(ModelBuilder modelBuilder);
}
