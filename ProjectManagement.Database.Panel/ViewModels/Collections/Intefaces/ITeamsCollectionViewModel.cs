using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;

public interface ITeamsCollectionViewModel
{
    public List<ITeamViewModel> Teams { get; set; }
    public ITeam TeamToAdd { get; set; }

    public void AddTeam();
}
