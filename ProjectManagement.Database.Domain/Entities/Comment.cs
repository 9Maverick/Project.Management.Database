using ProjectManagement.Database.Domain.Interfaces;

namespace ProjectManagement.Database.Domain.Entities;

public class Comment : IComment
{
    public uint Id { get; set; }
    public uint UserId { get; set; }
    public User? User { get; set; }
    public uint TicketId { get; set; }
    public Ticket? Ticket { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Comment() { }

    public Comment(IComment comment)
    {
        SetComment(comment);
    }

    public void SetComment(IComment comment)
    {
        UserId = comment.UserId;
        TicketId = comment.TicketId;
        Text = comment.Text;
        CreatedAt = comment.CreatedAt;
    }
}
