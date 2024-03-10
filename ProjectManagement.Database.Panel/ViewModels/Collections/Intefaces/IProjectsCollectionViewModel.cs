using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;

public interface IProjectsCollectionViewModel
{
    public List<IProjectViewModel> Projects { get; set; }
    public IProject ProjectToAdd { get; set; }

    public void AddProject();
}
