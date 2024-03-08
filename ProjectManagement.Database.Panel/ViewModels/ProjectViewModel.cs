using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.ViewModels.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels;

public class ProjectViewModel : IProjectViewModel
{
    private Action<IProjectViewModel> OnDeleted;

    private DatabaseContext _context;
    private Project _project;

    public IProject Project { get; set; }
    public bool IsDeleted { get; set; } = false;
    public bool IsEditing { get; set; } = false;

    public ProjectViewModel(Project project, DatabaseContext context, Action<IProjectViewModel> onDeleted)
    {
        _context = context;
        _project = project;

        Project = project;
        OnDeleted = onDeleted;
    }

    public void Cancel()
    {
        Project = _project;
        IsEditing = false;
    }

    public void Delete()
    {
        _context.Projects.Remove(_project);
        _context.SaveChanges();

        IsDeleted = true;

        OnDeleted(this);
    }

    public void Edit()
    {
        Project = new ProjectModel(_project);
        IsEditing = true;
    }

    public void Save()
    {
        _project.SetProject(Project);
        _context.SaveChanges();

        Project = _project;
        IsEditing = false;
    }
}
