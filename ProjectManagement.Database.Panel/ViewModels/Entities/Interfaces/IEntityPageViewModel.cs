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

public interface IChildEntityPageViewModel<T> : IChildEntityViewModel<T>
{
    public Dictionary<uint?, string> ParentSource { get; }
    public bool IsLoaded { get; }
    public bool IsEditing { get; }
    public void Edit();
    public void Save();
    public void Cancel();
    public void Delete();
}
