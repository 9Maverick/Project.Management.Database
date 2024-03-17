using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Page;

public class ProjectPageViewModel : IEntityPageViewModel<IProject>
{

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
        }
    }
    public IProject Entity { get; set; }
    public IProject? Parent { get; set; }
    public Dictionary<uint?, string> ParentIdNames { get; set; }
    public bool IsLoaded { get; set; } = false;
    public bool IsEditing { get; set; } = false;

    public ProjectPageViewModel(DatabaseContext context)
    {
        _context = context;
    }

    public void Cancel()
    {
        Entity = _project;
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
        Entity = new ProjectModel(_project);
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

        var project = _context.Projects.Find(Id);

        if (project == null) return;

        _project = project;
        Entity = _project;

        IsLoaded = true;
    }
}
