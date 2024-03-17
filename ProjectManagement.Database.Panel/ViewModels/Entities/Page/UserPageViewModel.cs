using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Page;

public class UserPageViewModel : IUserViewModel
{

    private DatabaseContext _context;
    private User _user;

    private uint _id;

    public uint Id
    {
        get => _id;
        set
        {
            _id = value;
            LoadUser();
        }
    }
    public IUser Entity { get; set; }
    public IUser? Parent { get; set; }
    public Dictionary<uint?, string> ParentIdNames { get; set; }
    public bool IsLoaded { get; set; } = false;
    public bool IsEditing { get; set; } = false;

    public UserPageViewModel(DatabaseContext context)
    {
        _context = context;
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

        IsLoaded = false;
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

        IsEditing = false;

        LoadUser();
    }

    private void LoadUser()
    {
        if (Id == 0) return;

        var user = _context.Users.Find(Id);

        if (user == null) return;

        _user = user;
        Entity = _user;

        IsLoaded = true;
    }
}
