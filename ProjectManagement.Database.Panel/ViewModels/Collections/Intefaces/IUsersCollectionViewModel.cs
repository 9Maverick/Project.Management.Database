using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;

public interface IUsersCollectionViewModel
{
    public List<IUserViewModel> Entities { get; set; }
    public IUser EntityToAdd { get; set; }

    public void AddEntity();
}
