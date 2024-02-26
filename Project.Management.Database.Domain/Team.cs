namespace Project.Management.Database.Domain;

public class Team
{
	public uint Id { get; set; }
	public string Name { get; set; }
	public uint? ParentId { get; set; }
	public Team? Parent { get; set; }
	public List<Team> Children { get; set; }
	public List<User> Users { get; set; }

}
