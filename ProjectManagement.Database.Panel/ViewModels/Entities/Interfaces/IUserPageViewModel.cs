using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

public interface IUserPageViewModel : IEntityPageViewModel<IUser>
{
    public List<Team> Teams { get; set; }
    public List<Team> TeamsSource { get; set; }

    public IEntityCollectionViewModel<ITeam> TeamCollection { get; }
    INestedEntityCollectionViewModel<ITicket, IProject> CreatedTicketCollection { get; }
    INestedEntityCollectionViewModel<ITicket, IProject> AssignedTicketCollection { get; }
    INestedEntityCollectionViewModel<ITicket, IProject> TrackingTicketCollection { get; }
}
