using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Row;

public class UserRowViewModel : IEntityViewModel<IUser>
{
    private User _user;

    public uint Id { get; set; }
    public IUser Entity
    {
        get => _user;
        set
        {
            return;
        }
    }

    public UserRowViewModel(User user)
    {
        _user = user;

        Id = _user.Id;
    }
}
