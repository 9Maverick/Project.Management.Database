using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities;

public class TeamViewModel : ITeamViewModel
{
    private Action<ITeamViewModel> OnDeleted;

    private DatabaseContext _context;
    private Team _team;

    public ITeam Entity { get; set; }
    public ITeam? Parent { get; set; }
    public bool IsDeleted { get; set; } = false;
    public bool IsEditing { get; set; } = false;

    public TeamViewModel(Team team, DatabaseContext context, Action<ITeamViewModel> onDeleted)
    {
        _context = context;
        _team = team;

        Entity = _team;

        LoadParent();
    }

    public void Cancel()
    {
        Entity = _team;
        IsEditing = false;
    }

    public void Delete()
    {
        _context.Teams.Remove(_team);
        _context.SaveChanges();

        IsDeleted = true;

        OnDeleted(this);
    }

    public void Edit()
    {
        Entity = new TeamModel(_team);
        IsEditing = true;
    }

    public void Save()
    {
        _team.SetTeam(Entity);
        _context.SaveChanges();

        Entity = _team;
        IsEditing = false;


    }

    private void LoadParent()
    {
        var parentId = Entity.ParentId;
        if (parentId == null || parentId == 0) return;

        Parent = _context.Teams
            .Where(e => e.Id == parentId)
            .FirstOrDefault();
    }
}
