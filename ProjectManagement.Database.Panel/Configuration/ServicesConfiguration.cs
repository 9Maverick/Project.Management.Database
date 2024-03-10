using ProjectManagement.Database.Panel.ViewModels.Collections;
using ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;

namespace ProjectManagement.Database.Panel.Configuration;
public static class ServicesConfiguration
{
    public static void AddViewModels(this IServiceCollection services)
    {
        services.AddScoped<IProjectsCollectionViewModel, ProjectsCollectionViewModel>();
        services.AddScoped<IUsersCollectionViewModel, UsersCollectionViewModel>();
        services.AddScoped<ITeamsCollectionViewModel, TeamsCollectionViewModel>();
    }
}
