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

public class TicketPageViewModel : ITicketPageViewModel
{
    private UserCollectionViewModel _trackingUserCollection;
    private TicketCollectionViewModel _childrenCollection;
    private TicketCollectionViewModel _linkedToCollection;
    private TicketCollectionViewModel _linkedFromCollection;
    private CommentCollectionViewModel _commentCollection;

    private DatabaseContext _context;
    private Ticket _ticket;

    private uint _id;

    public uint Id
    {
        get => _id;
        set
        {
            _id = value;
            LoadTicket();
            SetCollectionsSettings();
        }
    }
    public ITicket Entity { get; set; }
    public ITicket? Parent { get; set; }
    public Dictionary<uint?, string> ParentSource { get; set; }

    public List<User> TrackingUsers { get; set; }
    public List<User> UsersSource { get; set; }



    public IEntityCollectionViewModel<IUser> TrackingUserCollection { get => _trackingUserCollection; }
    public INestedEntityCollectionViewModel<ITicket, IProject> ChildrenCollection { get => _childrenCollection; }
    public INestedEntityCollectionViewModel<ITicket, IProject> LinkedToCollection { get => _linkedToCollection; }

    public INestedEntityCollectionViewModel<ITicket, IProject> LinkedFromCollection { get => _linkedFromCollection; }
    public IOwnedEntityCollectionViewModel<IComment> CommentCollection { get => _commentCollection; }

    public bool IsLoaded { get; set; } = false;
    public bool IsEditing { get; set; } = false;
    public List<Ticket> LinkedTo { get; set; }
    public List<Ticket> TicketsSource { get; private set; }


    public IProject Master { get; private set; }
    public Dictionary<uint, string> MasterSource { get; private set; }

    public IUser Owner { get; private set; }

    public Dictionary<uint, string> OwnerSource { get; private set; }
    public IUser? Assignee { get; private set; }


    public TicketPageViewModel(DatabaseContext context)
    {
        _context = context;

        _trackingUserCollection = new UserCollectionViewModel(context);
        _childrenCollection = new TicketCollectionViewModel(context);
        _linkedToCollection = new TicketCollectionViewModel(context);
        _linkedFromCollection = new TicketCollectionViewModel(context);
        _commentCollection = new CommentCollectionViewModel(context);
    }

    #region Controls

    public void Cancel()
    {
        SetView(_ticket);
        IsEditing = false;
    }

    public void Delete()
    {
        _context.Tickets.Remove(_ticket);
        _context.SaveChanges();

        IsLoaded = false;
    }

    public void Edit()
    {
        SetView(new TicketModel(_ticket));
        IsEditing = true;
    }

    public void Save()
    {
        SetModel();
        _context.SaveChanges();

        IsEditing = false;

        LoadTicket();
    }

    #endregion

    private void LoadTicket()
    {
        if (Id == 0) return;

        var ticket = _context.Tickets
            .Include(ticket => ticket.TrackingUsers)
            .Include(ticket => ticket.LinkedFrom)
            .Include(ticket => ticket.LinkedTo)
            .Include(ticket => ticket.Children)
            .Include(ticket => ticket.Comments)
            .Where(ticket => ticket.Id == Id)
            .FirstOrDefault();

        if (ticket == null) return;

        _ticket = ticket;
        SetView(_ticket);

        LoadParent();
        LoadParentVariants();

        LoadOwner();
        LoadOwnerVariants();

        LoadMaster();
        LoadMasterVariants();

        LoadSources();

        IsLoaded = true;
    }

    private void SetView(ITicket ticket)
    {
        Entity = ticket;

        Assignee = _ticket.Assignee;

        TrackingUsers = _ticket.TrackingUsers?.ToList();
        var children = _ticket.Children?.ToList();

        LinkedTo = _ticket.LinkedTo?.ToList();
        var linkedFromTickets = _ticket.LinkedFrom?.ToList();

        var comments = _ticket.Comments?.ToList();

        _trackingUserCollection.SetUsers(TrackingUsers);
        _childrenCollection.SetTickets(children);

        _linkedToCollection.SetTickets(LinkedTo);
        _linkedFromCollection.SetTickets(linkedFromTickets);

        _commentCollection.SetComments(comments);
    }

    private void SetModel()
    {
        _ticket.SetTicket(Entity);
        _ticket.LinkedTo = LinkedTo;
        _ticket.TrackingUsers = TrackingUsers;
    }

    private void SetCollectionsSettings()
    {
        var userCollectionSettings = new CollectionSettingsModel<IUser>()
        {
            IsImmutable = true,
        };
        _trackingUserCollection.Settings = userCollectionSettings;

        var childrenCollectionSettings = new CollectionSettingsModel<ITicket>()
        {
            IsImmutable = true,
        };
        _childrenCollection.Settings = childrenCollectionSettings;

        var ticketsCollectionSettings = new CollectionSettingsModel<ITicket>()
        {
            IsImmutable = true,
        };
        _linkedToCollection.Settings = ticketsCollectionSettings;
        _linkedFromCollection.Settings = ticketsCollectionSettings;

        var commentsCollectionSettings = new CollectionSettingsModel<IComment>()
        {
            DefaultValue = new Comment()
            {
                TicketId = _ticket.Id
            }
        };
        _commentCollection.Settings = commentsCollectionSettings;
    }

    private void LoadSources()
    {
        UsersSource = _context.Users.ToList();

        TicketsSource = _context.Tickets
            .Where(ticket => ticket.Id != Id)
            .ToList();
    }

    private void LoadParentVariants()
    {
        ParentSource = new Dictionary<uint?, string>();

        var ticketsList = _context.Tickets
            .Where(ticket => ticket.Id != Id)
            .ToList();

        foreach (var ticket in ticketsList)
        {
            ParentSource.Add(ticket.Id, ticket.Number);
        }
    }

    private void LoadOwnerVariants()
    {
        OwnerSource = new Dictionary<uint, string>();

        var userList = _context.Users
            .ToList();

        foreach (var user in userList)
        {
            OwnerSource.Add(user.Id, user.Name);
        }
    }

    private void LoadMasterVariants()
    {
        MasterSource = new Dictionary<uint, string>();

        var projectsList = _context.Projects
            .ToList();

        foreach (var project in projectsList)
        {
            MasterSource.Add(project.Id, project.Name);
        }
    }

    private void LoadParent()
    {
        var parentId = Entity.ParentId;
        if (parentId == null || parentId == 0) return;

        Parent = _context.Tickets
            .Where(e => e.Id == parentId)
            .FirstOrDefault();
    }
    private void LoadOwner()
    {
        var ownerId = Entity.CreatorId;
        if (ownerId == null || ownerId == 0) return;

        Owner = _context.Users
            .Where(user => user.Id == ownerId)
            .FirstOrDefault();
    }
    private void LoadMaster()
    {
        var masterId = Entity.ProjectId;
        if (masterId == null || masterId == 0) return;

        Master = _context.Projects
            .Where(project => project.Id == masterId)
            .FirstOrDefault();
    }
}
