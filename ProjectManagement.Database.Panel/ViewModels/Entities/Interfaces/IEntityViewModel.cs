using ProjectManagement.Database.Domain.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

public interface IEntityViewModel<T>
{
    public uint Id { get; set; }
    public T Entity { get; }
}

public interface IChildEntityViewModel<T> : IEntityViewModel<T>
{
    public T? Parent { get; }
}

public interface IOwnedEntityViewModel<T> : IEntityViewModel<T>
{
    public IUser Owner { get; set; }
}

public interface IOwnedChildEntityViewModel<T> : IChildEntityViewModel<T>
{
    public IUser Owner { get; }
}

public interface INestedEntityViewModel<T, TMaster> : IOwnedEntityViewModel<T>
{
    public TMaster Master { get; }
}

public interface INestedChildEntityViewModel<T, TMaster> : IOwnedChildEntityViewModel<T>
{
    public TMaster Master { get; }
}