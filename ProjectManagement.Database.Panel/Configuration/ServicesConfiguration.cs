using ProjectManagement.Database.Panel.ViewModels.Collections;
using ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Page;

namespace ProjectManagement.Database.Panel.Configuration;
public static class ServicesConfiguration
{
    public static void AddViewModels(this IServiceCollection services)
    {
        services.AddTransient<IProjectsCollectionViewModel, ProjectsCollectionViewModel>();
        services.AddTransient<IUsersCollectionViewModel, UsersCollectionViewModel>();
        services.AddTransient<ITeamsCollectionViewModel, TeamsCollectionViewModel>();
        services.AddTransient<IEditableTeamViewModel, TeamPageViewModel>();
    }
}
