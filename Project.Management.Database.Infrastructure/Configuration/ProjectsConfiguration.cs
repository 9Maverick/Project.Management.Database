using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Management.Database.Data.Configuration;

public class ProjectsConfiguration : IEntityTypeConfiguration<Domain.Project>
{
	public void Configure(EntityTypeBuilder<Domain.Project> entity)
	{
		entity.Property(e => e.Sequence)
			.HasDefaultValue(1);
	}
}
