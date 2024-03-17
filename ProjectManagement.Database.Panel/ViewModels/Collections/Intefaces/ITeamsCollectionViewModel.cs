using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;

public interface ITeamsCollectionViewModel
{
    public List<IChildEntityViewModel<ITeam>> Entities { get; set; }
    public Dictionary<uint?, string> ParentIdNames { get; set; }
    public ITeam EntityToAdd { get; set; }

    public void AddEntity();
}
