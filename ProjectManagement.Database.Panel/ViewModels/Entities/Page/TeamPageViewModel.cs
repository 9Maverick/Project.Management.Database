using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Page;

public class TeamPageViewModel : IEditableTeamViewModel
{

    private DatabaseContext _context;
    private Team _team;

    private uint _id;

    public uint Id
    {
        get => _id;
        set
        {
            _id = value;
            LoadTeam();
        }
    }
    public ITeam Entity { get; set; }
    public ITeam? Parent { get; set; }
    public Dictionary<uint?, string> ParentIdNames { get; set; }
    public bool IsLoaded { get; set; } = false;
    public bool IsEditing { get; set; } = false;

    public TeamPageViewModel(DatabaseContext context)
    {
        _context = context;
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

        IsLoaded = false;
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

        IsEditing = false;

        LoadTeam();
    }

    private void LoadTeam()
    {
        if (Id == 0) return;

        var team = _context.Teams.Find(Id);

        if (team == null) return;

        _team = team;
        Entity = _team;

        LoadParentVariants();
        LoadParent();

        IsLoaded = true;
    }

    private void LoadParentVariants()
    {
        ParentIdNames = new Dictionary<uint?, string>();

        var teamsList = _context.Teams
            .Where(team => team.Id != Id)
            .ToList();

        foreach (var team in teamsList)
        {
            ParentIdNames.Add(team.Id, team.Name);
        }
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
