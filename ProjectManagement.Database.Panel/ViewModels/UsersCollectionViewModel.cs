using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.ViewModels.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels;

public class UsersCollectionViewModel : IUsersCollectionViewModel
{
    private DatabaseContext _context;

    public List<IUserViewModel> Users { get; set; }
    public IUser UserToAdd { get; set; }

    public UsersCollectionViewModel(DatabaseContext context)
    {
        _context = context;

        UserToAdd = new UserModel();

        LoadUsers();
    }

    public void AddUser()
    {
        if (string.IsNullOrWhiteSpace(UserToAdd.Name))
            return;

        var user = new User(UserToAdd);

        _context.Users.Add(user);
        _context.SaveChanges();

        Users.Add(new UserViewModel(user, _context, OnUserDeleted));

        UserToAdd = new UserModel();
    }

    public void OnUserDeleted(IUserViewModel user)
    {
        Users.Remove(user);
    }

    private void LoadUsers()
    {
        Users = new List<IUserViewModel>();

        var usersList = _context.Users.ToList();

        foreach (var project in usersList)
        {
            Users.Add(new UserViewModel(project, _context, OnUserDeleted));
        }
    }
}
