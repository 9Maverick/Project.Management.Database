using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.Models;
using ProjectManagement.Database.Panel.ViewModels.Collections;
using ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Page;

public class TeamPageViewModel : ITeamPageViewModel
{
    private UserCollectionViewModel _userCollectionViewModel;
    private TeamCollectionViewModel _childrenCollectionViewModel;
    private DatabaseContext _context;
    private Team _team;

    private uint _id;

    public uint Id
    {
        get => _id;
        set
        {
            _id = value;
            LoadTeam();
        }
    }
    public ITeam Entity { get; set; }
    public ITeam? Parent { get; set; }
    public Dictionary<uint?, string> ParentIdNames { get; set; }

    public List<User> Users { get; set; }
    public List<Team> Children { get; set; }
    public List<User> UsersSource { get; set; }


    public IChildEntityCollectionViewModel<ITeam> ChildrenCollection { get => _childrenCollectionViewModel; }
    public IEntityCollectionViewModel<IUser> UserCollection { get => _userCollectionViewModel; }

    public bool IsLoaded { get; set; } = false;
    public bool IsEditing { get; set; } = false;

    public TeamPageViewModel(DatabaseContext context)
    {
        _context = context;
        _userCollectionViewModel = new UserCollectionViewModel(context);
        _childrenCollectionViewModel = new TeamCollectionViewModel(context);
    }

    #region Controls

    public void Cancel()
    {
        SetView(_team);
        IsEditing = false;
    }

    public void Delete()
    {
        _context.Teams.Remove(_team);
        _context.SaveChanges();

        IsLoaded = false;
    }

    public void Edit()
    {
        SetView(new TeamModel(_team));
        IsEditing = true;
    }

    public void Save()
    {
        SetModel();
        _context.SaveChanges();

        IsEditing = false;

        LoadTeam();
    }

    #endregion

    private void LoadTeam()
    {
        if (Id == 0) return;

        var team = _context.Teams.Find(Id);

        if (team == null) return;

        _team = team;
        SetView(_team);

        LoadParentVariants();
        LoadParent();
        LoadSources();

        IsLoaded = true;
    }

    private void SetView(ITeam team)
    {
        Entity = team;

        Users = _team.Users?.ToList();
        Children = _team.Children?.ToList();

        _userCollectionViewModel.SetUsers(Users);
        _childrenCollectionViewModel.SetTeams(Children);
    }

    private void SetModel()
    {
        _team.SetTeam(Entity);
        _team.Users = Users;
        _team.Children = Children;
    }

    private void SetCollectionsSettings()
    {
        var userCollectionSettings = new CollectionSettingsModel<IUser>()
        {
            IsImmutable = true,
        };
        _userCollectionViewModel.Settings = userCollectionSettings;

        var childrenCollectionSettings = new CollectionSettingsModel<ITeam>()
        {
            DefaultValue = new TeamModel()
            {
                ParentId = Id
            }
        };
        _childrenCollectionViewModel.Settings = childrenCollectionSettings;
    }

    private void LoadSources()
    {
        UsersSource = _context.Users.ToList();
    }

    private void LoadParentVariants()
    {
        ParentIdNames = new Dictionary<uint?, string>();

        var teamsList = _context.Teams
            .Where(team => team.Id != Id)
            .ToList();

        foreach (var team in teamsList)
        {
            ParentIdNames.Add(team.Id, team.Name);
        }
    }

    private void LoadParent()
    {
        var parentId = Entity.ParentId;
        if (parentId == null || parentId == 0) return;

        Parent = _context.Teams
            .Where(e => e.Id == parentId)
            .FirstOrDefault();
    }
}
