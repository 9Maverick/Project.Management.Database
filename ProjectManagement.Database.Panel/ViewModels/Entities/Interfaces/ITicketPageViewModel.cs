using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

public interface ITicketPageViewModel : INestedEntityPageViewModel<ITicket, IProject>, IChildEntityPageViewModel<ITicket>
{
    public IUser? Assignee { get; }
    public List<User> TrackingUsers { get; set; }
    public List<User> UsersSource { get; }
    public List<Ticket> LinkedTo { get; set; }
    public List<Ticket> TicketsSource { get; }

    public IEntityCollectionViewModel<IUser> TrackingUserCollection { get; }
    public INestedEntityCollectionViewModel<IComment, ITicket> CommentsCollection { get; }
    public INestedEntityCollectionViewModel<ITicket, IProject> ChildrenCollection { get; }
    public INestedEntityCollectionViewModel<ITicket, IProject> LinkedToCollection { get; }
    public INestedEntityCollectionViewModel<ITicket, IProject> LinkedFromCollection { get; }
}
