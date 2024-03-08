using ProjectManagement.Database.Domain.Interfaces;

namespace ProjectManagement.Database.Domain.Entities;

public class User : IUser
{
    public uint Id { get; set; }
    public string Name { get; set; }
    public List<Ticket> CreatedTasks { get; set; }
    public List<Ticket> AssignedTasks { get; set; }
    public List<Ticket> TrackingTasks { get; set; }
    public List<Team> Teams { get; set; }

    public User()
    {
        CreatedTasks = new List<Ticket>();
        AssignedTasks = new List<Ticket>();
        TrackingTasks = new List<Ticket>();
        Teams = new List<Team>();
    }

    public User(IUser user) : this()
    {
        SetUser(user);
    }

    public void SetUser(IUser user)
    {
        Name = user.Name;
    }
}