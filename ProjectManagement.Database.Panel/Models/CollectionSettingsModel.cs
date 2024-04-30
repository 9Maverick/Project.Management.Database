namespace ProjectManagement.Database.Panel.Models;

public class CollectionSettingsModel<T>
{
	public T? DefaultValue { get; set; }
	public bool IsImmutable { get; set; } = false;
}