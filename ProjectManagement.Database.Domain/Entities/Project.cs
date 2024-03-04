namespace ProjectManagement.Database.Domain.Entities;

public class Project
{
    public uint Id { get; set; }
    public string Name { get; set; }
    public string Abbreviation { get; set; }
    public uint Sequence { get; set; }
    public List<Ticket> Tickets { get; set; }
}