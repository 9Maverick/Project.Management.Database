using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.Models;
using ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Row;

namespace ProjectManagement.Database.Panel.ViewModels.Collections;

public class ProjectCollectionViewModel : IEntityCollectionViewModel<IProject>
{
    private DatabaseContext _context;
    private List<Project> _projects;

    public CollectionSettingsModel<IProject> Settings { get; set; }

    public List<IEntityViewModel<IProject>> Entities { get; set; }
    public IProject EntityToAdd { get; set; }

    public ProjectCollectionViewModel(DatabaseContext context)
    {
        _context = context;

        Settings = new CollectionSettingsModel<IProject>();

        SetEntityToAdd();

        LoadProjects();
    }

    public void SaveEntity()
    {
        if (!IsProjectValid(EntityToAdd))
            return;

        var project = new Project(EntityToAdd);

        _context.Projects.Add(project);
        _context.SaveChanges();

        SetEntityToAdd();

        LoadProjects();
    }

    public void SetProjects(List<Project> projects)
    {
        _projects = projects;
        LoadProjects();
    }

    private List<Project> GetProjects()
    {
        return _projects ?? _context.Projects.ToList();
    }

    private void LoadProjects()
    {
        Entities = new List<IEntityViewModel<IProject>>();

        var projectsList = GetProjects();

        foreach (var project in projectsList)
        {
            Entities.Add(new ProjectRowViewModel(project));
        }
    }

    private void SetEntityToAdd()
    {

        EntityToAdd = new ProjectModel();

        if (Settings.DefaultValue == null) return;

        var defaultValue = Settings.DefaultValue;

        if (!string.IsNullOrWhiteSpace(defaultValue.Name))
        {
            EntityToAdd.Name = defaultValue.Name;
        }

        if (!string.IsNullOrWhiteSpace(defaultValue.Abbreviation))
        {
            EntityToAdd.Abbreviation = defaultValue.Abbreviation;
        }
    }

    private bool IsProjectValid(IProject project)
    {
        return !string.IsNullOrWhiteSpace(project.Name)
            && !string.IsNullOrWhiteSpace(project.Abbreviation);
    }
}
