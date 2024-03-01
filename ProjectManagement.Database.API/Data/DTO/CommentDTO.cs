namespace ProjectManagement.Database.API.Data.DTO;

public class CommentDTO
{
	public uint Id { get; set; }
	public uint UserId { get; set; }
	public uint TicketId { get; set; }
	public string Text { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
