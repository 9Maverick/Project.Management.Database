using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;

namespace ProjectManagement.Database.Panel.ViewModels.Entities.Row;

public class CommentRowViewModel : IOwnedEntityPageViewModel<IComment>
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

    public Dictionary<uint, string> OwnerSource { get; private set; }

    public bool IsLoaded { get; set; }
    public bool IsEditing { get; set; }

    public CommentRowViewModel(Comment comment, DatabaseContext context)
    {
        _context = context;
        _comment = comment;

        Id = _comment.Id;

        LoadOwner();
    }

    #region Controls

    public void Cancel()
    {
        Entity = _comment;
        IsEditing = false;
    }

    public void Delete()
    {
        _context.Comments.Remove(_comment);
        _context.SaveChanges();

        IsLoaded = false;
    }

    public void Edit()
    {
        Entity = new CommentModel(_comment);
        IsEditing = true;
    }

    public void Save()
    {
        _comment.SetComment(Entity);
        _context.SaveChanges();

        IsEditing = false;

        LoadComment();
    }

    #endregion

    private void LoadComment()
    {
        if (Id == 0) return;

        var comment = _context.Comments.Find(Id);

        if (comment == null) return;

        _comment = comment;
        Entity = _comment;

        LoadOwner();
        LoadOwnerVariants();

        IsLoaded = true;
    }

    private void LoadOwner()
    {
        var ownerId = Entity.UserId;
        if (ownerId == null || ownerId == 0) return;

        Owner = _context.Users
            .Where(user => user.Id == ownerId)
            .FirstOrDefault();
    }
    private void LoadOwnerVariants()
    {
        OwnerSource = new Dictionary<uint, string>();

        var userList = _context.Users
            .ToList();

        foreach (var user in userList)
        {
            OwnerSource.Add(user.Id, user.Name);
        }
    }
}
