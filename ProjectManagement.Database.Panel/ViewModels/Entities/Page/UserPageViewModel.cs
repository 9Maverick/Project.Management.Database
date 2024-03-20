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
    private TicketCollectionViewModel _createdTicketCollection;
    private TicketCollectionViewModel _assignedTicketCollection;
    private TicketCollectionViewModel _trackingTicketCollection;

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

    public IEntityCollectionViewModel<ITeam> TeamCollection { get => _teamCollection; }

    public bool IsLoaded { get; set; } = false;
    public bool IsEditing { get; set; } = false;

    public INestedEntityCollectionViewModel<ITicket, IProject> CreatedTicketCollection => _createdTicketCollection;

    public INestedEntityCollectionViewModel<ITicket, IProject> AssignedTicketCollection => _assignedTicketCollection;

    public INestedEntityCollectionViewModel<ITicket, IProject> TrackingTicketCollection => _trackingTicketCollection;

    public UserPageViewModel(DatabaseContext context)
    {
        _context = context;
        _teamCollection = new TeamCollectionViewModel(_context);

        _createdTicketCollection = new TicketCollectionViewModel(_context);
        _assignedTicketCollection = new TicketCollectionViewModel(_context);
        _trackingTicketCollection = new TicketCollectionViewModel(_context);
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
            .Include(user => user.CreatedTasks)
            .Include(user => user.AssignedTasks)
            .Include(user => user.TrackingTasks)
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

        var createdTickets = _user.CreatedTasks?.ToList();
        var assignedTickets = _user.AssignedTasks?.ToList();
        var trackingTickets = _user?.TrackingTasks?.ToList();

        _teamCollection.SetTeams(Teams);

        _createdTicketCollection.SetTickets(createdTickets);
        _assignedTicketCollection.SetTickets(assignedTickets);
        _trackingTicketCollection.SetTickets(trackingTickets);
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

        var ticketCollectionSettings = new CollectionSettingsModel<ITicket>()
        {
            IsImmutable = true,
        };
        _createdTicketCollection.Settings = ticketCollectionSettings;
        _assignedTicketCollection.Settings = ticketCollectionSettings;
        _trackingTicketCollection.Settings = ticketCollectionSettings;
    }
}
