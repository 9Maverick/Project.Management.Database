using ProjectManagement.Database.Domain.Enums;

namespace ProjectManagement.Database.Domain.Interfaces;

public interface ITicket
{
	public string Number { get; set; }
	public uint ProjectId { get; set; }
	public uint CreatorId { get; set; }
	public uint? AssigneeId { get; set; }
	public uint? ParentId { get; set; }
	public TicketType Type { get; set; }

	public string? Title { get; set; }
	public string? Description { get; set; }

	public Priority Priority { get; set; }
	public int StoryPoints { get; set; }
	public Status Status { get; set; }

	public DateTime CreatedAt { get; set; }
}