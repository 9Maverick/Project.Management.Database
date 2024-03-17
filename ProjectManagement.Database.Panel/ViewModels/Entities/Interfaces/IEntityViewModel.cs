namespace ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

public interface IEntityViewModel<T>
{
    public uint Id { get; set; }
    public T Entity { get; set; }
}

public interface IChildEntityViewModel<T> : IEntityViewModel<T>
{
    public T? Parent { get; }
}