using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;
using ProjectManagement.Database.Panel.ViewModels.Entities;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Collections;

public class ProjectsCollectionViewModel : IProjectsCollectionViewModel
{
    private DatabaseContext _context;

    public List<IProjectViewModel> Entities { get; set; }
    public IProject EntityToAdd { get; set; }

    public ProjectsCollectionViewModel(DatabaseContext context)
    {
        _context = context;

        EntityToAdd = new ProjectModel();

        LoadProjects();
    }

    public void Add()
    {
        if (!IsProjectValid(EntityToAdd))
            return;

        var project = new Project(EntityToAdd);

        _context.Projects.Add(project);
        _context.SaveChanges();

        Entities.Add(new ProjectViewModel(project, _context, OnProjectDeleted));

        EntityToAdd = new ProjectModel();
    }

    public void OnProjectDeleted(IProjectViewModel project)
    {
        Entities.Remove(project);
    }

    private void LoadProjects()
    {
        Entities = new List<IProjectViewModel>();

        var projectsList = _context.Projects.ToList();

        foreach (var project in projectsList)
        {
            Entities.Add(new ProjectViewModel(project, _context, OnProjectDeleted));
        }
    }

    private bool IsProjectValid(IProject project)
    {
        return !string.IsNullOrWhiteSpace(project.Name)
            && !string.IsNullOrWhiteSpace(project.Abbreviation);
    }
}
