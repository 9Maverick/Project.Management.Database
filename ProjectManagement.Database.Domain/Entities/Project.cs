using ProjectManagement.Database.Domain.Interfaces;

namespace ProjectManagement.Database.Domain.Entities;

public class Project : IProject
{
    public uint Id { get; set; }
    public string Name { get; set; }
    public string Abbreviation { get; set; }
    public uint Sequence { get; set; }
    public List<Ticket> Tickets { get; set; }

    public Project()
    {
        Tickets = new List<Ticket>();
    }

    public Project(IProject project) : this()
    {
        Tickets = new List<Ticket>();

        SetProject(project);
    }

    public void SetProject(IProject project)
    {
        Name = project.Name;
        Abbreviation = project.Abbreviation;
    }
}