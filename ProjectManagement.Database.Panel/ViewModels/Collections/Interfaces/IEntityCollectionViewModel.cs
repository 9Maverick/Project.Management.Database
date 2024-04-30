﻿using ProjectManagement.Database.Panel.Models;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;

public interface IEntityCollectionViewModel<T>
{
    public CollectionSettingsModel<T> Settings { get; set; }
    public List<IEntityViewModel<T>> Entities { get; }
    public Dictionary<uint?, string> ParentSource { get; }
    public T EntityToAdd { get; set; }
    public void SaveEntity();
}

public interface IOwnedEntityCollectionViewModel<T> : IEntityCollectionViewModel<T>
{
    new public List<IOwnedEntityViewModel<T>> Entities { get; }
    public Dictionary<uint, string> OwnerSource { get; }
}

public interface INestedEntityCollectionViewModel<T, TMaster> : IOwnedEntityCollectionViewModel<T>
{
    new public List<INestedEntityViewModel<T, TMaster>> Entities { get; }
    public Dictionary<uint, string> MasterSource { get; }
}