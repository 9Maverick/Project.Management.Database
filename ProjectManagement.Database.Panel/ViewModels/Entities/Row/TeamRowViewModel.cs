using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Row;

public class TeamRowViewModel : IEntityViewModel<ITeam>
{

    private DatabaseContext _context;
    private Team _team;

    public uint Id { get; set; }
    public ITeam Entity
    {
        get => _team;
        set
        {
            return;
        }
    }
    public ITeam? Parent { get; private set; }

    public TeamRowViewModel(Team team, DatabaseContext context)
    {
        _context = context;
        _team = team;

        Id = _team.Id;

        LoadParent();

    }
    private void LoadParent()
    {
        var parentId = Entity.ParentId;
        if (parentId == null || parentId == 0) return;

        Parent = _context.Teams
            .Where(team => team.Id == parentId)
            .FirstOrDefault();
    }
}
