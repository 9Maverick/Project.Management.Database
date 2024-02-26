using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Management.Database.Domain;

namespace Project.Management.Database.Data.Configuration;

public class TeamsConfiguration : IEntityTypeConfiguration<Team>
{
	public void Configure(EntityTypeBuilder<Team> entity)
	{
		entity.HasKey(e => e.Id);

		entity.Property(e => e.Name)
			.IsRequired();
				
		entity.HasOne(e => e.Parent)
			.WithMany(e => e.Children)
			.HasForeignKey(e => e.ParentId)
			.OnDelete(DeleteBehavior.ClientSetNull);

		entity.HasMany(e => e.Users)
			.WithMany(e => e.Teams);
	}
}
