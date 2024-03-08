namespace ProjectManagement.Database.Domain.Interfaces;

public interface IComment
{
	public uint UserId { get; set; }
	public uint TicketId { get; set; }
	public string Text { get; set; }
	public DateTime CreatedAt { get; set; }
}
