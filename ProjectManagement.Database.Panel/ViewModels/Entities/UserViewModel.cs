using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities;

public class UserViewModel : IUserViewModel
{
    private Action<IUserViewModel> OnDeleted;

    private DatabaseContext _context;
    private User _user;

    public uint Id { get; set; }
    public IUser Entity { get; set; }
    public bool IsLoaded { get; set; } = false;
    public bool IsEditing { get; set; } = false;

    public UserViewModel(User project, DatabaseContext context, Action<IUserViewModel> onDeleted)
    {
        _context = context;
        _user = project;

        Entity = project;
        OnDeleted = onDeleted;
    }

    public void Cancel()
    {
        Entity = _user;
        IsEditing = false;
    }

    public void Delete()
    {
        _context.Users.Remove(_user);
        _context.SaveChanges();

        IsLoaded = true;

        OnDeleted(this);
    }

    public void Edit()
    {
        Entity = new UserModel(_user);
        IsEditing = true;
    }

    public void Save()
    {
        _user.SetUser(Entity);
        _context.SaveChanges();

        Entity = _user;
        IsEditing = false;
    }
}
