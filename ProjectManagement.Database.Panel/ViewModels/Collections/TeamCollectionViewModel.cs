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

        EntityToAdd = new TeamModel();

        Load();
    }

    public void AddEntity()
    {
        if (!IsTeamValid(EntityToAdd))
            return;

        var team = new Team(EntityToAdd);

        _context.Teams.Add(team);
        _context.SaveChanges();

        OnTeamAdded(team);

        EntityToAdd = new TeamModel();
    }

    public void OnTeamAdded(Team team)
    {
        Entities.Add(new TeamRowViewModel(team, _context));

        ParentIdNames.Add(team.Id, team.Name);
    }

    public void Load()
    {
        Entities = new List<IChildEntityViewModel<ITeam>>();
        ParentIdNames = new Dictionary<uint?, string>();

        var teamsList = _context.Teams.ToList();

        foreach (var team in teamsList)
        {
            OnTeamAdded(team);
        }
    }

    private bool IsTeamValid(ITeam team)
    {
        return !string.IsNullOrWhiteSpace(team.Name);
    }
}
