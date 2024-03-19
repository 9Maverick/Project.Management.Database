namespace ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

public interface IEntityPageViewModel<T> : IEntityViewModel<T>
{
    public bool IsLoaded { get; set; }
    public bool IsEditing { get; set; }
    public void Edit();
    public void Save();
    public void Cancel();
    public void Delete();
}

public interface IChildEntityPageViewModel<T> : IEntityPageViewModel<T>, IEntityViewModel<T>
{
    public Dictionary<uint?, string> ParentSource { get; }
}

public interface IOwnedEntityPageViewModel<T> : IEntityPageViewModel<T>, IOwnedEntityViewModel<T>
{
    public Dictionary<uint, string> OwnerSource { get; }
}

public interface INestedEntityPageViewModel<T, TMaster> : IOwnedEntityPageViewModel<T>, INestedEntityViewModel<T, TMaster>
{
    public Dictionary<uint, string> MasterSource { get; }
}
