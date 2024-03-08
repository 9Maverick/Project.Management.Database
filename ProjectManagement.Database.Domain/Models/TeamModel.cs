using ProjectManagement.Database.Domain.Interfaces;

namespace ProjectManagement.Database.Domain.Models;

public class TeamModel : ITeam
{
    public string Name { get; set; }
    public uint? ParentId { get; set; }

    public TeamModel() { }
    public TeamModel(ITeam team)
    {
        Name = team.Name;
        ParentId = team.ParentId;
    }
}
