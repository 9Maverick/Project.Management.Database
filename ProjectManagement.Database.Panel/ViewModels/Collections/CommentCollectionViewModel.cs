using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.Models;
using ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Row;

namespace ProjectManagement.Database.Panel.ViewModels.Collections;

public class CommentCollectionViewModel : IOwnedEntityCollectionViewModel<IComment>
{
    private DatabaseContext _context;
    private List<Comment> _comments;
    private CollectionSettingsModel<IComment> _settings;

    public CollectionSettingsModel<IComment> Settings
    {
        get => _settings;
        set
        {
            _settings = value;
            SetEntityToAdd();
        }
    }
    public List<IOwnedEntityViewModel<IComment>> Entities { get; set; }
    public Dictionary<uint?, string> ParentSource => throw new NotImplementedException();
    public IComment EntityToAdd { get; set; }

    public Dictionary<uint, string> OwnerSource { get; private set; }

    List<IEntityViewModel<IComment>> IEntityCollectionViewModel<IComment>.Entities => throw new NotImplementedException();

    public CommentCollectionViewModel(DatabaseContext context)
    {
        _context = context;

        Settings = new CollectionSettingsModel<IComment>();

        SetEntityToAdd();

        LoadComments();
    }

    public void SaveEntity()
    {
        if (!IsCommentValid(EntityToAdd))
            return;

        var comment = new Comment(EntityToAdd);

        comment.CreatedAt = DateTime.Now;

        _context.Comments.Add(comment);
        _context.SaveChanges();

        AddToCollection(comment);

        SetEntityToAdd();

        LoadComments();
    }

    public void SetComments(List<Comment> comments)
    {
        _comments = comments ?? new List<Comment>();
        LoadComments();
    }

    private List<Comment> GetComments()
    {
        return _comments ?? _context.Comments.ToList();
    }
    private void AddToCollection(Comment comment)
    {
        Entities.Add(new CommentRowViewModel(comment, _context));
    }

    private void LoadComments()
    {
        Entities = new List<IOwnedEntityViewModel<IComment>>();

        var commentsList = GetComments();

        foreach (var comment in commentsList)
        {
            AddToCollection(comment);
        }

        LoadSources();
    }

    private void LoadSources()
    {
        OwnerSource = new Dictionary<uint, string>();

        var users = _context.Users.ToList();
        foreach (var user in users)
        {
            OwnerSource.Add(user.Id, user.Name);
        }
    }


    private void SetEntityToAdd()
    {

        EntityToAdd = new CommentModel();

        if (Settings.DefaultValue == null) return;

        var defaultValue = Settings.DefaultValue;

        if (!string.IsNullOrWhiteSpace(defaultValue.Text))
        {
            EntityToAdd.Text = defaultValue.Text;
        }

        if (defaultValue.UserId != null && defaultValue.UserId > 0)
        {
            EntityToAdd.UserId = defaultValue.UserId;
        }

        if (defaultValue.TicketId > 0)
        {
            EntityToAdd.TicketId = defaultValue.TicketId;
        }
    }

    private bool IsCommentValid(IComment comment)
    {
        return !string.IsNullOrWhiteSpace(comment.Text)
            && comment.UserId > 0
            && comment.TicketId > 0;
    }
}
