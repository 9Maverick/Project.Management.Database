namespace ProjectManagement.Database.API.Data.DTO;
public class TeamDTO
{
	public uint Id { get; set; }
	public string Name { get; set; }
	public uint? ParentId { get; set; }
}
