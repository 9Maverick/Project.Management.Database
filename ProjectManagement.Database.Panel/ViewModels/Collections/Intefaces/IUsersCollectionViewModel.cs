using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;

public interface IUsersCollectionViewModel
{
    public List<IUserViewModel> Users { get; set; }
    public IUser UserToAdd { get; set; }

    public void AddUser();
}
