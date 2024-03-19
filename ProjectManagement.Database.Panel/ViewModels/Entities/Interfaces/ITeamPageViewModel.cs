using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

public interface ITeamPageViewModel : IChildEntityPageViewModel<ITeam>
{
    public List<User> Users { get; set; }
    public List<User> UsersSource { get; set; }

    public IEntityCollectionViewModel<ITeam> ChildrenCollection { get; }
    public IEntityCollectionViewModel<IUser> UserCollection { get; }
}
