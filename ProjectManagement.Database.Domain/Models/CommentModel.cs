using ProjectManagement.Database.Domain.Interfaces;

namespace ProjectManagement.Database.Domain.Models;

public class CommentModel : IComment
{
	public uint UserId { get; set; }
	public uint TicketId { get; set; }
	public string Text { get; set; }
	public DateTime CreatedAt { get; set; }

	public CommentModel() { }

	public CommentModel(IComment comment)
	{
		UserId = comment.UserId;
		TicketId = comment.TicketId;
		Text = comment.Text;
		CreatedAt = comment.CreatedAt;
	}
}