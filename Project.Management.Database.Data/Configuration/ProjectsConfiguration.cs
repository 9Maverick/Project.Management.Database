using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Management.Database.Data.Configuration;

public class ProjectsConfiguration : IEntityTypeConfiguration<Domain.Project>
{
	public void Configure(EntityTypeBuilder<Domain.Project> entity)
	{
		entity.HasKey(e => e.Id);

		entity.Property(e => e.Abbreviation)
			.IsRequired();

		entity.Property(e => e.Name)
			.IsRequired();
	}
}
