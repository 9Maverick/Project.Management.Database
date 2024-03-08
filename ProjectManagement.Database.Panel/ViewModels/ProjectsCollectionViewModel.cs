using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.ViewModels.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels;

public class ProjectsCollectionViewModel : IProjectsCollectionViewModel
{
    private DatabaseContext _context;

    public List<IProjectViewModel> Projects { get; set; }
    public IProject ProjectToAdd { get; set; }

    public ProjectsCollectionViewModel(DatabaseContext context)
    {
        _context = context;

        ProjectToAdd = new ProjectModel();

        LoadProjects();
    }

    public void AddProject()
    {
        if (!IsProjectValid(ProjectToAdd))
            return;

        var project = new Project(ProjectToAdd);

        _context.Projects.Add(project);
        _context.SaveChanges();

        Projects.Add(new ProjectViewModel(project, _context, OnProjectDeleted));

        ProjectToAdd = new ProjectModel();
    }

    public void OnProjectDeleted(IProjectViewModel project)
    {
        Projects.Remove(project);
    }

    private void LoadProjects()
    {
        Projects = new List<IProjectViewModel>();

        var projectsList = _context.Projects.ToList();

        foreach (var project in projectsList)
        {
            Projects.Add(new ProjectViewModel(project, _context, OnProjectDeleted));
        }
    }

    private bool IsProjectValid(IProject project)
    {
        return !string.IsNullOrWhiteSpace(project.Name)
            && !string.IsNullOrWhiteSpace(project.Abbreviation);
    }
}
