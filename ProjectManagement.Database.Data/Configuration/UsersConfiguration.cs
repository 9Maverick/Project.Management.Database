using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Database.Domain.Entities;

namespace ProjectManagement.Database.Data.Configuration;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> entity)
	{
		entity.HasKey(x => x.Id);

		entity.Property(x => x.Name)
			.IsRequired();
	}
}