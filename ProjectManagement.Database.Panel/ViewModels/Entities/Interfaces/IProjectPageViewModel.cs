using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

public interface IProjectPageViewModel : IEntityPageViewModel<IProject>
{
	INestedEntityCollectionViewModel<ITicket, IProject> TicketCollection { get; }
}