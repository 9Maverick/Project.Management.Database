using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.Models;
using ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Row;

namespace ProjectManagement.Database.Panel.ViewModels.Collections;

public class TicketCollectionViewModel : INestedEntityCollectionViewModel<ITicket, IProject>
{
	private DatabaseContext _context;
	private List<Ticket> _tickets;

	public CollectionSettingsModel<ITicket> Settings { get; set; }
	public List<INestedEntityViewModel<ITicket, IProject>> Entities { get; set; }
	public Dictionary<uint?, string> ParentSource { get; set; }
	public ITicket EntityToAdd { get; set; }

	public Dictionary<uint, string> MasterSource { get; private set; }

	public Dictionary<uint, string> OwnerSource { get; private set; }

	List<IOwnedEntityViewModel<ITicket>> IOwnedEntityCollectionViewModel<ITicket>.Entities => throw new NotImplementedException();

	List<IEntityViewModel<ITicket>> IEntityCollectionViewModel<ITicket>.Entities => throw new NotImplementedException();

	public TicketCollectionViewModel(DatabaseContext context)
	{
		_context = context;

		Settings = new CollectionSettingsModel<ITicket>();

		SetEntityToAdd();

		LoadTickets();
	}

	public void SaveEntity()
	{
		if(!IsTicketValid(EntityToAdd))
			return;

		var ticket = new Ticket(EntityToAdd);

		ticket.Number = null;

		_context.Tickets.Add(ticket);
		_context.SaveChanges();

		AddToCollection(ticket);

		SetEntityToAdd();

		LoadTickets();
	}

	public void SetTickets(List<Ticket> tickets)
	{
		_tickets = tickets ?? new List<Ticket>();
		LoadTickets();
	}

	private List<Ticket> GetTickets()
	{
		return _tickets ?? _context.Tickets.ToList();
	}
	private void AddToCollection(Ticket ticket)
	{
		Entities.Add(new TicketRowViewModel(ticket, _context));

		ParentSource.Add(ticket.Id, ticket.Number);
	}

	private void LoadTickets()
	{
		Entities = new List<INestedEntityViewModel<ITicket, IProject>>();
		ParentSource = new Dictionary<uint?, string>();

		var ticketsList = GetTickets();

		foreach(var ticket in ticketsList)
		{
			AddToCollection(ticket);
		}

		LoadSources();
	}

	private void LoadSources()
	{
		OwnerSource = new Dictionary<uint, string>();
		MasterSource = new Dictionary<uint, string>();

		var users = _context.Users.ToList();
		foreach(var user in users)
		{
			OwnerSource.Add(user.Id, user.Name);
		}

		var projects = _context.Projects.ToList();
		foreach(var project in projects)
		{
			MasterSource.Add(project.Id, project.Name);
		}
	}


	private void SetEntityToAdd()
	{

		EntityToAdd = new TicketModel();

		if(Settings.DefaultValue == null)
			return;

		var defaultValue = Settings.DefaultValue;

		if(!string.IsNullOrWhiteSpace(defaultValue.Title))
		{
			EntityToAdd.Title = defaultValue.Title;
		}

		if(defaultValue.ParentId != null && defaultValue.ParentId > 0)
		{
			EntityToAdd.ParentId = defaultValue.ParentId;
		}

		if(defaultValue.ProjectId > 0)
		{
			EntityToAdd.ProjectId = defaultValue.ProjectId;
		}

		if(defaultValue.CreatorId > 0)
		{
			EntityToAdd.CreatorId = defaultValue.CreatorId;
		}


		EntityToAdd.AssigneeId = defaultValue.AssigneeId;
		EntityToAdd.Type = defaultValue.Type;
		EntityToAdd.Description = defaultValue.Description;
		EntityToAdd.Priority = defaultValue.Priority;
		EntityToAdd.StoryPoints = defaultValue.StoryPoints;
		EntityToAdd.Status = defaultValue.Status;
	}

	private bool IsTicketValid(ITicket ticket)
	{
		return !string.IsNullOrWhiteSpace(ticket.Title)
			&& ticket.CreatorId > 0
			&& ticket.ProjectId > 0;
	}
}