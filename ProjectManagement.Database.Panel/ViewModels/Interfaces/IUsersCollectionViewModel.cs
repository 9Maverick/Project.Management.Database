using ProjectManagement.Database.Domain.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Interfaces;

public interface IUsersCollectionViewModel
{
    public List<IUserViewModel> Users { get; set; }
    public IUser UserToAdd { get; set; }

    public void AddUser();
}
