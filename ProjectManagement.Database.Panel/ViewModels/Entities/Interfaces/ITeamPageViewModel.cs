using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

public interface ITeamPageViewModel : IChildEntityPageViewModel<ITeam>
{
    public List<Team> Children { get; set; }
    public List<User> Users { get; set; }
}
