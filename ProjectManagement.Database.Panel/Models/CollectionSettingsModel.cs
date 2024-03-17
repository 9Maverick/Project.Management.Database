namespace ProjectManagement.Database.Panel.Models;

public class CollectionSettingsModel<T>
{
    public Action? Filter { get; set; }
    public T? DefaultValue { get; set; }
}
