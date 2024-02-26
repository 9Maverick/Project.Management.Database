using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Management.Database.Domain;

namespace Project.Management.Database.Data.Configuration;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> entity)
	{
		entity.HasKey(x => x.Id);

		entity.Property(x => x.Name)
			.IsRequired();
	}
}
