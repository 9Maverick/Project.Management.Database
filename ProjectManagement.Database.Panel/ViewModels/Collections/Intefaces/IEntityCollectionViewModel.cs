using ProjectManagement.Database.Panel.Models;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;

public interface IEntityCollectionViewModel<T>
{
    public CollectionSettingsModel<T> Settings { get; set; }
    public List<IEntityViewModel<T>> Entities { get; set; }
    public T EntityToAdd { get; set; }
    public void SaveEntity();
}

public interface IChildEntityCollectionViewModel<T>
{
    public CollectionSettingsModel<T> Settings { get; set; }
    public List<IChildEntityViewModel<T>> Entities { get; set; }
    public Dictionary<uint?, string> ParentIdNames { get; set; }
    public T EntityToAdd { get; set; }
    public void SaveEntity();
}
