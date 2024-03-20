using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Row;

public class CommentRowViewModel : IOwnedEntityViewModel<IComment>
{
    private DatabaseContext _context;
    private Comment _comment;

    public uint Id { get; set; }
    public IComment Entity
    {
        get => _comment;
        set
        {
            return;
        }
    }

    public IComment? Parent => throw new NotImplementedException();

    public IUser Owner { get; private set; }

    public CommentRowViewModel(Comment comment)
    {
        _comment = comment;

        Id = _comment.Id;

        LoadOwner();
    }

    private void LoadOwner()
    {
        var ownerId = Entity.UserId;
        if (ownerId == null || ownerId == 0) return;

        Owner = _context.Users
            .Where(user => user.Id == ownerId)
            .FirstOrDefault();
    }
}
