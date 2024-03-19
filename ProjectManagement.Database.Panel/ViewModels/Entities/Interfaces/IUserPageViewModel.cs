using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

public interface IUserPageViewModel : IEntityPageViewModel<IUser>
{
    public List<Team> Teams { get; set; }
    public List<Team> TeamsSource { get; set; }

    public IChildEntityCollectionViewModel<ITeam> TeamCollection { get; }
}
