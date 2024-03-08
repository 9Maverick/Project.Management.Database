using ProjectManagement.Database.Domain.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Interfaces;

public interface IProjectsCollectionViewModel
{
    public List<IProjectViewModel> Projects { get; set; }
    public IProject ProjectToAdd { get; set; }

    public void AddProject();
}
