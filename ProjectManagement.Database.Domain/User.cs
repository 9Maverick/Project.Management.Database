namespace ProjectManagement.Database.Domain;

public class User
{
	public uint Id { get; set; }
	public string Name { get; set; }
	public List<Ticket> CreatedTasks { get; set; }
	public List<Ticket> AssignedTasks { get; set; }
	public List<Ticket> TrackingTasks { get; set; }
	public List<Team> Teams { get; set; }
}