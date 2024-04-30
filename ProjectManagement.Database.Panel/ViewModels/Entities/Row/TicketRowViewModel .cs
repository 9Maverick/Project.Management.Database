using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Row;

public class TicketRowViewModel : INestedEntityViewModel<ITicket, IProject>
{

	private DatabaseContext _context;
	private Ticket _team;

	public uint Id { get; set; }
	public ITicket Entity
	{
		get => _team;
		set
		{
			return;
		}
	}
	public ITicket? Parent { get; private set; }

	public IProject Master { get; private set; }

	public IUser Owner { get; private set; }

	public TicketRowViewModel(Ticket team, DatabaseContext context)
	{
		_context = context;
		_team = team;

		Id = _team.Id;

		LoadParent();
		LoadOwner();
		LoadMaster();
	}
	private void LoadParent()
	{
		var parentId = Entity.ParentId;
		if(parentId == null || parentId == 0)
			return;

		Parent = _context.Tickets
			.Where(ticket => ticket.Id == parentId)
			.FirstOrDefault();
	}
	private void LoadOwner()
	{
		var ownerId = Entity.CreatorId;
		if(ownerId == null || ownerId == 0)
			return;

		Owner = _context.Users
			.Where(user => user.Id == ownerId)
			.FirstOrDefault();
	}
	private void LoadMaster()
	{
		var masterId = Entity.ProjectId;
		if(masterId == null || masterId == 0)
			return;

		Master = _context.Projects
			.Where(project => project.Id == masterId)
			.FirstOrDefault();
	}
}