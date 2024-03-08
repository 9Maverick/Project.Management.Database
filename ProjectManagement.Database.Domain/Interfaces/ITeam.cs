namespace ProjectManagement.Database.Domain.Interfaces;

public interface ITeam
{
    public string Name { get; set; }
    public uint? ParentId { get; set; }
}
