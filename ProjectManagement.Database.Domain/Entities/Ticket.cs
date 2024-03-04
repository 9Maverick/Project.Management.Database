using ProjectManagement.Database.Domain.Enums;

namespace ProjectManagement.Database.Domain.Entities;

public class Ticket
{
    public uint Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public uint ProjectId { get; set; }
    public Project? Project { get; set; }
    public uint CreatorId { get; set; }
    public User? Creator { get; set; }
    public uint? AssigneeId { get; set; }
    public User? Assignee { get; set; }
    public uint? ParentId { get; set; }
    public Ticket? Parent { get; set; }
    public TicketType Type { get; set; } = TicketType.Story;

    public string? Title { get; set; }
    public string? Description { get; set; }

    public Priority Priority { get; set; } = Priority.None;
    public int StoryPoints { get; set; } = 0;
    public Status Status { get; set; } = Status.Open;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Ticket> Children { get; set; }
    public List<Ticket> LinkedTo { get; set; }
    public List<Ticket> LinkedFrom { get; set; }
    public List<User> TrackingUsers { get; set; }
    public List<Comment> Comments { get; set; }

    //TODO: add modification history
    //TODO: add custom fields
}