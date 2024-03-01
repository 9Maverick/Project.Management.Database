using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Database.Domain;

namespace ProjectManagement.Database.Data.Configuration;

public class ProjectsConfiguration : IEntityTypeConfiguration<Project>
{
	public void Configure(EntityTypeBuilder<Project> entity)
	{
		entity.HasKey(e => e.Id);

		entity.Property(e => e.Abbreviation)
			.IsRequired();

		entity.Property(e => e.Name)
			.IsRequired();
	}
}
