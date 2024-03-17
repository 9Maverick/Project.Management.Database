using ProjectManagement.Database.Domain.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

public interface IEditableViewModel
{
    public bool IsLoaded { get; set; }
    public bool IsEditing { get; set; }
    public void Edit();
    public void Save();
    public void Cancel();
    public void Delete();
}

public interface IEditableChildViewModel : IEditableViewModel
{
    public Dictionary<uint?, string> ParentIdNames { get; set; }
}
public interface IEditableTeamViewModel : IChildEntityViewModel<ITeam>, IEditableChildViewModel;
