namespace ProjectManagement.Database.Domain;

public class Project
{
	public uint Id { get; set; }
	public string Abbreviation { get; set; }
	public string Name { get; set; }
	public uint Sequence { get; set; }
	public List<Ticket> Tickets { get; set; }
}