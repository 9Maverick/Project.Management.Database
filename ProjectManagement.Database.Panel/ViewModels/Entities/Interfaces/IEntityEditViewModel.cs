using ProjectManagement.Database.Domain.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

public interface IEntityEditViewModel
{
    public bool IsDeleted { get; set; }
    public bool IsEditing { get; set; }
    public void Edit();
    public void Save();
    public void Cancel();
    public void Delete();
}

public interface IProjectViewModel : IEntityViewModel<IProject>, IEntityEditViewModel;
public interface IUserViewModel : IEntityViewModel<IUser>, IEntityEditViewModel;
public interface ITeamViewModel : IChildEntityViewModel<ITeam>, IEntityEditViewModel;
