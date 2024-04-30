using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Database.Data.Generators;
using ProjectManagement.Database.Domain.Entities;

namespace ProjectManagement.Database.Data.Configuration;

public class TicketsConfiguration : IEntityTypeConfiguration<Ticket>
{
	public void Configure(EntityTypeBuilder<Ticket> entity)
	{
		entity.HasKey(e => e.Id);

		entity.HasOne(e => e.Project)
			.WithMany(e => e.Tickets)
			.HasForeignKey(e => e.ProjectId)
			.OnDelete(DeleteBehavior.Cascade)
			.IsRequired();

		entity.HasOne(e => e.Creator)
			.WithMany(e => e.CreatedTasks)
			.HasForeignKey(e => e.CreatorId)
			.OnDelete(DeleteBehavior.ClientSetNull);

		entity.HasOne(e => e.Assignee)
			.WithMany(e => e.AssignedTasks)
			.HasForeignKey(e => e.AssigneeId)
			.OnDelete(DeleteBehavior.ClientSetNull)
			.IsRequired(false);

		entity.HasOne(e => e.Parent)
			.WithMany(e => e.Children)
			.HasForeignKey(e => e.ParentId)
			.OnDelete(DeleteBehavior.ClientSetNull);

		entity.Property(e => e.Title)
			.IsRequired();

		entity.Property(e => e.Type)
			.HasConversion<string>();

		entity.Property(e => e.Priority)
			.HasConversion<string>();

		entity.Property(e => e.Status)
			.HasConversion<string>();

		entity.HasMany(e => e.LinkedFrom)
			.WithMany(e => e.LinkedTo);

		entity.HasMany(e => e.TrackingUsers)
			.WithMany(e => e.TrackingTasks);

		entity.Property(e => e.Number)
			.HasValueGenerator<TicketNumberGenerator>();

	}
}