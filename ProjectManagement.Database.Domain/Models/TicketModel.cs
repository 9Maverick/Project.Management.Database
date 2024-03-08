using ProjectManagement.Database.Domain.Enums;
using ProjectManagement.Database.Domain.Interfaces;

namespace ProjectManagement.Database.Domain.Models;

public class TicketModel : ITicket
{
	public string Number { get; set; } = string.Empty;
	public uint ProjectId { get; set; }
	public uint CreatorId { get; set; }
	public uint? AssigneeId { get; set; }
	public uint? ParentId { get; set; }
	public TicketType Type { get; set; } = TicketType.Story;

	public string? Title { get; set; }
	public string? Description { get; set; }

	public Priority Priority { get; set; } = Priority.None;
	public int StoryPoints { get; set; } = 0;
	public Status Status { get; set; } = Status.Open;

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public TicketModel() { }
	public TicketModel(ITicket ticket)
	{
		Number = ticket.Number;
		ProjectId = ticket.ProjectId;
		CreatorId = ticket.CreatorId;
		AssigneeId = ticket.AssigneeId;
		ParentId = ticket.ParentId;
		Type = ticket.Type;
		Title = ticket.Title;
		Description = ticket.Description;
		Priority = ticket.Priority;
		StoryPoints = ticket.StoryPoints;
		Status = ticket.Status;
		CreatedAt = ticket.CreatedAt;
	}
}
