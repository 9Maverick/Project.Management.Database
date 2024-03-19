using Microsoft.EntityFrameworkCore;
using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.Models;
using ProjectManagement.Database.Panel.ViewModels.Collections;
using ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Page;

public class UserPageViewModel : IUserPageViewModel
{
    private TeamCollectionViewModel _teamCollection;

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
            SetCollectionsSettings();
        }
    }
    public IUser Entity { get; set; }
    public IUser? Parent { get; set; }
    public Dictionary<uint?, string> ParentIdNames { get; set; }

    public List<Team> Teams { get; set; }
    public List<Team> TeamsSource { get; set; }

    public IChildEntityCollectionViewModel<ITeam> TeamCollection { get => _teamCollection; }

    public bool IsLoaded { get; set; } = false;
    public bool IsEditing { get; set; } = false;

    public UserPageViewModel(DatabaseContext context)
    {
        _context = context;
        _teamCollection = new TeamCollectionViewModel(_context);
    }

    #region Controls

    public void Cancel()
    {
        SetView(_user);
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
        SetView(new UserModel(_user));
        IsEditing = true;
    }

    public void Save()
    {
        SetModel();
        _context.SaveChanges();

        IsEditing = false;

        LoadUser();
    }

    #endregion

    private void LoadUser()
    {
        if (Id == 0) return;

        var user = _context.Users
            .Include(user => user.Teams)
            .Where(user => user.Id == Id)
            .FirstOrDefault();

        if (user == null) return;

        _user = user;
        SetView(_user);

        LoadSources();

        IsLoaded = true;
    }

    private void SetView(IUser user)
    {
        Entity = user;

        Teams = _user.Teams?.ToList();

        _teamCollection.SetTeams(Teams);
    }

    private void SetModel()
    {
        _user.SetUser(Entity);
        _user.Teams = Teams;
    }

    private void LoadSources()
    {
        TeamsSource = _context.Teams.ToList();
    }

    private void SetCollectionsSettings()
    {
        var teamCollectionSettings = new CollectionSettingsModel<ITeam>()
        {
            IsImmutable = true,
        };
        _teamCollection.Settings = teamCollectionSettings;
    }
}
