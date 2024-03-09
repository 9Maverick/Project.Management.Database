using ProjectManagement.Database.Domain.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Interfaces;

public interface ITeamsCollectionViewModel
{
    public List<ITeamViewModel> Teams { get; set; }
    public ITeam TeamToAdd { get; set; }

    public void AddTeam();
}
