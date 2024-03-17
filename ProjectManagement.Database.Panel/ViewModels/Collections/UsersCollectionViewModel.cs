using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Row;

namespace ProjectManagement.Database.Panel.ViewModels.Collections;

public class UsersCollectionViewModel : IEntityCollectionViewModel<IUser>
{
    private DatabaseContext _context;

    public List<IEntityViewModel<IUser>> Entities { get; set; }
    public IUser EntityToAdd { get; set; }

    public UsersCollectionViewModel(DatabaseContext context)
    {
        _context = context;

        EntityToAdd = new UserModel();

        LoadUsers();
    }

    public void AddEntity()
    {
        if (!IsUserValid(EntityToAdd)) return;

        var user = new User(EntityToAdd);

        _context.Users.Add(user);
        _context.SaveChanges();

        Entities.Add(new UserRowViewModel(user));

        EntityToAdd = new UserModel();
    }

    private void LoadUsers()
    {
        Entities = new List<IEntityViewModel<IUser>>();

        var usersList = _context.Users.ToList();

        foreach (var project in usersList)
        {
            Entities.Add(new UserRowViewModel(project));
        }
    }

    private bool IsUserValid(IUser user)
    {
        return !string.IsNullOrWhiteSpace(user.Name);
    }
}
