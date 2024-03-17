using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities;

public class ProjectViewModel : IProjectViewModel
{
    private Action<IProjectViewModel> OnDeleted;

    private DatabaseContext _context;
    private Project _project;

    public uint Id { get; set; }
    public IProject Entity { get; set; }
    public bool IsLoaded { get; set; } = false;
    public bool IsEditing { get; set; } = false;

    public ProjectViewModel(Project project, DatabaseContext context, Action<IProjectViewModel> onDeleted)
    {
        _context = context;
        _project = project;

        Entity = project;
        OnDeleted = onDeleted;
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

        IsLoaded = true;

        OnDeleted(this);
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

        Entity = _project;
        IsEditing = false;
    }
}
