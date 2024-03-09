using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.ViewModels.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels;

public class TeamsCollectionViewModel : ITeamsCollectionViewModel
{
    private DatabaseContext _context;

    public List<ITeamViewModel> Teams { get; set; }
    public ITeam TeamToAdd { get; set; }

    public TeamsCollectionViewModel(DatabaseContext context)
    {
        _context = context;

        TeamToAdd = new TeamModel();

        LoadTeams();
    }

    public void AddTeam()
    {
        if (!IsTeamValid(TeamToAdd))
            return;

        var team = new Team(TeamToAdd);

        _context.Teams.Add(team);
        _context.SaveChanges();

        Teams.Add(new TeamViewModel(team, _context, OnTeamDeleted));

        TeamToAdd = new TeamModel();
    }

    public void OnTeamDeleted(ITeamViewModel team)
    {
        Teams.Remove(team);
    }

    private void LoadTeams()
    {
        Teams = new List<ITeamViewModel>();

        var teamsList = _context.Teams.ToList();

        foreach (var team in teamsList)
        {
            Teams.Add(new TeamViewModel(team, _context, OnTeamDeleted));
        }
    }

    private bool IsTeamValid(ITeam team)
    {
        return !string.IsNullOrWhiteSpace(team.Name);
    }
}
