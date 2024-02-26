using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Management.Database.Domain;
using Project.Management.Database.Domain.Enums;

namespace Project.Management.Database.Data.Configuration;

public class TicketsConfiguration : IEntityTypeConfiguration<Ticket>
{
	public void Configure(EntityTypeBuilder<Ticket> entity)
	{
		entity.Property(e => e.Type)
			.HasDefaultValue(TicketType.Story);

		entity.Property(e => e.Priority)
			.HasDefaultValue(Priority.None);

		entity.Property(e => e.Status)
			.HasDefaultValue(Status.Open);

	}
}
