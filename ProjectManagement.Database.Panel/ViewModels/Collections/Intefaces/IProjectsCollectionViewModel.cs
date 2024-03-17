using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;

public interface IProjectsCollectionViewModel
{
    public List<IProjectViewModel> Entities { get; set; }
    public IProject EntityToAdd { get; set; }

    public void Add();
}
