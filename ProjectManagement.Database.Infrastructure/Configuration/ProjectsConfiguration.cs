using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Database.Domain.Entities;

namespace ProjectManagement.Database.Data.Configuration;

public class ProjectsConfiguration : IEntityTypeConfiguration<Project>
{
	public void Configure(EntityTypeBuilder<Project> entity)
	{
		entity.Property(e => e.Sequence)
			.HasDefaultValue(1);
	}
}