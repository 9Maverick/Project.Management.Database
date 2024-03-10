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

    public IUser User { get; set; }
    public bool IsDeleted { get; set; } = false;
    public bool IsEditing { get; set; } = false;

    public UserViewModel(User project, DatabaseContext context, Action<IUserViewModel> onDeleted)
    {
        _context = context;
        _user = project;

        User = project;
        OnDeleted = onDeleted;
    }

    public void Cancel()
    {
        User = _user;
        IsEditing = false;
    }

    public void Delete()
    {
        _context.Users.Remove(_user);
        _context.SaveChanges();

        IsDeleted = true;

        OnDeleted(this);
    }

    public void Edit()
    {
        User = new UserModel(_user);
        IsEditing = true;
    }

    public void Save()
    {
        _user.SetUser(User);
        _context.SaveChanges();

        User = _user;
        IsEditing = false;
    }
}
