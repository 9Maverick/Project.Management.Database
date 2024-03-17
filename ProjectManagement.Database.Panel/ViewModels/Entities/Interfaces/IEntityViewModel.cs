using ProjectManagement.Database.Domain.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

public interface IEntityViewModel<T>
{
    public uint Id { get; set; }
    public T Entity { get; set; }
}

public interface IProjectViewModel : IEntityViewModel<IProject>, IEditableViewModel;
public interface IUserViewModel : IEntityViewModel<IUser>, IEditableViewModel;
public interface ITeamViewModel : IChildEntityViewModel<ITeam>, IEditableViewModel;


public interface IChildEntityViewModel<T> : IEntityViewModel<T>
{
    public T? Parent { get; }
}