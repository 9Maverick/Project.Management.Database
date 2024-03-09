using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.ViewModels.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels;

public class TeamViewModel : ITeamViewModel
{
    private Action<ITeamViewModel> OnDeleted;

    private DatabaseContext _context;
    private Team _team;

    public ITeam Team { get; set; }
    public ITeam? Parent { get; set; }
    public bool IsDeleted { get; set; } = false;
    public bool IsEditing { get; set; } = false;

    public TeamViewModel(Team team, DatabaseContext context, Action<ITeamViewModel> onDeleted)
    {
        _context = context;
        _team = team;

        Team = _team;
        OnDeleted = onDeleted;

        LoadParent();
    }

    public void Cancel()
    {
        Team = _team;
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
        Team = new TeamModel(_team);
        IsEditing = true;
    }

    public void Save()
    {
        _team.SetTeam(Team);
        _context.SaveChanges();

        Team = _team;
        IsEditing = false;


    }

    private void LoadParent()
    {
        var parentId = Team.ParentId;
        if (parentId == null || parentId == 0) return;

        Parent = _context.Teams
            .Where(e => e.Id == parentId)
            .FirstOrDefault();
    }
}
