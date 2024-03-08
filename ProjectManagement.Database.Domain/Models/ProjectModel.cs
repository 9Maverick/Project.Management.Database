using ProjectManagement.Database.Domain.Interfaces;

namespace ProjectManagement.Database.Domain.Models;

public class ProjectModel : IProject
{
	public string Name { get; set; }
	public string Abbreviation { get; set; }

	public ProjectModel() { }
	public ProjectModel(IProject project)
	{
		Name = project.Name;
		Abbreviation = project.Abbreviation;
	}
}
