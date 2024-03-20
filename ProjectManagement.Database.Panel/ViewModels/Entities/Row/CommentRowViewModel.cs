using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Row;

public class CommentRowViewModel : INestedEntityViewModel<IComment, ITicket>
{

    private DatabaseContext _context;
    private Comment _team;

    public uint Id { get; set; }
    public IComment Entity
    {
        get => _team;
        set
        {
            return;
        }
    }

    public IComment? Parent => throw new NotImplementedException();

    public ITicket Master { get; private set; }

    public IUser Owner { get; private set; }

    public CommentRowViewModel(Comment team, DatabaseContext context)
    {
        _context = context;
        _team = team;

        Id = _team.Id;

        LoadOwner();
        LoadMaster();
    }
    private void LoadOwner()
    {
        var ownerId = Entity.UserId;
        if (ownerId == null || ownerId == 0) return;

        Owner = _context.Users
            .Where(user => user.Id == ownerId)
            .FirstOrDefault();
    }
    private void LoadMaster()
    {
        var masterId = Entity.TicketId;
        if (masterId == null || masterId == 0) return;

        Master = _context.Tickets
            .Where(project => project.Id == masterId)
            .FirstOrDefault();
    }
}
