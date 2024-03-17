using ProjectManagement.Database.Domain.Interfaces;

namespace ProjectManagement.Database.Domain.Entities;

public class Team : ITeam
{
    public uint Id { get; set; }
    public string Name { get; set; }
    public uint? ParentId { get; set; }
    public Team? Parent { get; set; }
    //TODO: Pass users to parent teams
    public List<Team> Children { get; set; }
    public List<User> Users { get; set; }

    public Team()
    {
        Children = new List<Team>();
    }

    public Team(ITeam team) : this()
    {
        SetTeam(team);
    }

    public void SetTeam(ITeam team)
    {
        Name = team.Name;
        ParentId = team.ParentId;
    }
}
