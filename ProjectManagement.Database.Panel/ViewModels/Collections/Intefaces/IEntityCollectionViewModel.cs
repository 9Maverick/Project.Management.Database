using ProjectManagement.Database.Panel.Models;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;

public interface IEntityCollectionViewModel<T>
{
    public CollectionSettingsModel<T> Settings { get; set; }
    public List<IEntityViewModel<T>> Entities { get; }
    public Dictionary<uint?, string> ParentIdNames { get; }
    public T EntityToAdd { get; set; }
    public void SaveEntity();
}

