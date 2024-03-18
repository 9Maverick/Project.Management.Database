﻿using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.Models;
using ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Row;

namespace ProjectManagement.Database.Panel.ViewModels.Collections;

public class UserCollectionViewModel : IEntityCollectionViewModel<IUser>
{
    private DatabaseContext _context;

    public CollectionSettingsModel<IUser> Settings { get; set; }
    public List<IEntityViewModel<IUser>> Entities { get; set; }
    public IUser EntityToAdd { get; set; }

    public UserCollectionViewModel(DatabaseContext context)
    {
        _context = context;

        Settings = new CollectionSettingsModel<IUser>();

        SetEntityToAdd();

        LoadUsers();
    }

    public void SaveEntity()
    {
        if (!IsUserValid(EntityToAdd)) return;

        var user = new User(EntityToAdd);

        _context.Users.Add(user);
        _context.SaveChanges();

        EntityToAdd = new UserModel();

        LoadUsers();
    }

    private void LoadUsers()
    {
        Entities = new List<IEntityViewModel<IUser>>();

        var usersList = _context.Users.ToList();

        foreach (var project in usersList)
        {
            Entities.Add(new UserRowViewModel(project));
        }
    }

    private void SetEntityToAdd()
    {

        EntityToAdd = new UserModel();

        if (Settings.DefaultValue == null) return;

        var defaultValue = Settings.DefaultValue;

        if (!string.IsNullOrWhiteSpace(defaultValue.Name))
        {
            EntityToAdd.Name = defaultValue.Name;
        }
    }

    private bool IsUserValid(IUser user)
    {
        return !string.IsNullOrWhiteSpace(user.Name);
    }
}