namespace ProjectManagement.Database.API.Data.DTO;

public class TicketDTO
{
	public uint Id { get; set; }
	public string? Number { get; set; } = string.Empty;
	public uint ProjectId { get; set; }
	public uint CreatorId { get; set; }
	public uint? AssigneeId { get; set; }
	public uint? ParentId { get; set; }
	public string? Type { get; set; }

	public string? Title { get; set; }
	public string? Description { get; set; }

	public string? Priority { get; set; }
	public int StoryPoints { get; set; } = 0;
	public string? Status { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}