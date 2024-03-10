using ProjectManagement.Database.Domain.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

public interface IUserViewModel
{
    public IUser User { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsEditing { get; set; }
    public void Edit();
    public void Save();
    public void Cancel();
    public void Delete();
}
