namespace ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

public interface IEditableEntityViewModel<T> : IEntityViewModel<T>
{
    public bool IsLoaded { get; set; }
    public bool IsEditing { get; set; }
    public void Edit();
    public void Save();
    public void Cancel();
    public void Delete();
}

public interface IEditableChildEntityViewModel<T> : IChildEntityViewModel<T>
{
    public Dictionary<uint?, string> ParentIdNames { get; set; }
    public bool IsLoaded { get; set; }
    public bool IsEditing { get; set; }
    public void Edit();
    public void Save();
    public void Cancel();
    public void Delete();
}
