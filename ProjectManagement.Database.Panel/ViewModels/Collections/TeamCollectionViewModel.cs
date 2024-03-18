using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.Models;
using ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Row;

namespace ProjectManagement.Database.Panel.ViewModels.Collections;

public class TeamCollectionViewModel : IChildEntityCollectionViewModel<ITeam>
{
    private DatabaseContext _context;

    public CollectionSettingsModel<ITeam> Settings { get; set; }
    public List<IChildEntityViewModel<ITeam>> Entities { get; set; }
    public Dictionary<uint?, string> ParentIdNames { get; set; }
    public ITeam EntityToAdd { get; set; }

    public TeamCollectionViewModel(DatabaseContext context)
    {
        _context = context;

        Settings = new CollectionSettingsModel<ITeam>();

        SetEntityToAdd();

        LoadTeams();
    }

    public void SaveEntity()
    {
        if (!IsTeamValid(EntityToAdd))
            return;

        var team = new Team(EntityToAdd);

        _context.Teams.Add(team);
        _context.SaveChanges();

        AddToCollection(team);

        SetEntityToAdd();

        LoadTeams();
    }

    public void AddToCollection(Team team)
    {
        Entities.Add(new TeamRowViewModel(team, _context));

        ParentIdNames.Add(team.Id, team.Name);
    }

    private void LoadTeams()
    {
        Entities = new List<IChildEntityViewModel<ITeam>>();
        ParentIdNames = new Dictionary<uint?, string>();

        var teamsList = _context.Teams.ToList();

        foreach (var team in teamsList)
        {
            AddToCollection(team);
        }
    }

    private void SetEntityToAdd()
    {

        EntityToAdd = new TeamModel();

        if (Settings.DefaultValue == null) return;

        var defaultValue = Settings.DefaultValue;

        if (!string.IsNullOrWhiteSpace(defaultValue.Name))
        {
            EntityToAdd.Name = defaultValue.Name;
        }
        if (defaultValue.ParentId != null && defaultValue.ParentId > 0)
        {
            EntityToAdd.ParentId = defaultValue.ParentId;
        }
    }

    private bool IsTeamValid(ITeam team)
    {
        return !string.IsNullOrWhiteSpace(team.Name);
    }
}
