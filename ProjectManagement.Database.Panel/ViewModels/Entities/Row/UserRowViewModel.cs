using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Row;

public class ProjectRowViewModel : IEntityViewModel<IProject>
{
	private Project _project;

	public uint Id { get; set; }
	public IProject Entity
	{
		get => _project;
		set
		{
			return;
		}
	}

	public IProject? Parent => throw new NotImplementedException();

	public ProjectRowViewModel(Project project)
	{
		_project = project;

		Id = _project.Id;
	}
}