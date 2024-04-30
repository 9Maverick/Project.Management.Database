using ProjectManagement.Database.Domain.Interfaces;

namespace ProjectManagement.Database.Domain.Models;

public class UserModel : IUser
{
	public string Name { get; set; }

	public UserModel() { }
	public UserModel(IUser user)
	{
		Name = user.Name;
	}
}