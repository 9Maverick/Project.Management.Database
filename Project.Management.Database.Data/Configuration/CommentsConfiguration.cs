using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Management.Database.Domain;

namespace Project.Management.Database.Data.Configuration;

public class CommentsConfiguration : IEntityTypeConfiguration<Comment>
{
	public void Configure(EntityTypeBuilder<Comment> entity)
	{
		entity.HasKey(e => e.Id);

		entity.HasOne(e => e.Ticket)
			.WithMany(e => e.Comments)
			.HasForeignKey(e => e.TicketId)
			.OnDelete(DeleteBehavior.Cascade)
			.IsRequired();

		entity.HasOne(e => e.User)
			.WithOne()
			.HasForeignKey<Comment>(e => e.UserId)
			.OnDelete(DeleteBehavior.ClientSetNull);

		entity.Property(e => e.Text)
			.IsRequired();
	}
}
