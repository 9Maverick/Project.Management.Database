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

public class ProjectPageViewModel : IProjectPageViewModel
{
    private TicketCollectionViewModel _ticketCollection;
    private DatabaseContext _context;
    private Project _project;

    private uint _id;

    public uint Id
    {
        get => _id;
        set
        {
            _id = value;
            LoadProject();
            SetCollectionsSettings();
        }
    }
    public IProject Entity { get; set; }
    public IProject? Parent { get; set; }
    public Dictionary<uint?, string> ParentIdNames { get; set; }
    public bool IsLoaded { get; set; } = false;
    public bool IsEditing { get; set; } = false;

    public INestedEntityCollectionViewModel<ITicket, IProject> TicketCollection { get => _ticketCollection; }

    public ProjectPageViewModel(DatabaseContext context)
    {
        _context = context;
        _ticketCollection = new TicketCollectionViewModel(context);
    }

    public void Cancel()
    {
        SetView(_project);
        IsEditing = false;
    }

    public void Delete()
    {
        _context.Projects.Remove(_project);
        _context.SaveChanges();

        IsLoaded = false;
    }

    public void Edit()
    {
        SetView(new ProjectModel(_project));
        IsEditing = true;
    }

    public void Save()
    {
        _project.SetProject(Entity);
        _context.SaveChanges();

        IsEditing = false;

        LoadProject();
    }

    private void LoadProject()
    {
        if (Id == 0) return;

        var project = _context.Projects
            .Include(project => project.Tickets)
            .Where(project => project.Id == Id)
            .FirstOrDefault();

        if (project == null) return;

        _project = project;
        SetView(_project);

        IsLoaded = true;
    }

    private void SetView(IProject project)
    {
        Entity = project;

        var tickets = _project.Tickets?.ToList();

        _ticketCollection.SetTickets(tickets);
    }


    private void SetCollectionsSettings()
    {
        var ticketCollectionSettings = new CollectionSettingsModel<ITicket>()
        {
            IsImmutable = true,
        };
        _ticketCollection.Settings = ticketCollectionSettings;
    }
}