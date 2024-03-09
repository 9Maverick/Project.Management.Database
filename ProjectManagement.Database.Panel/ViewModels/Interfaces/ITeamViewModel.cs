using ProjectManagement.Database.Domain.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Interfaces;

public interface ITeamViewModel
{
    public ITeam Team { get; set; }
    public ITeam? Parent { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsEditing { get; set; }
    public void Edit();
    public void Save();
    public void Cancel();
    public void Delete();
}
