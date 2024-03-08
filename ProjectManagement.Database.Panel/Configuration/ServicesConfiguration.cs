using ProjectManagement.Database.Panel.ViewModels;
using ProjectManagement.Database.Panel.ViewModels.Interfaces;

namespace ProjectManagement.Database.Panel.Configuration;
public static class ServicesConfiguration
{
    public static void AddViewModels(this IServiceCollection services)
    {
        services.AddScoped<IProjectsCollectionViewModel, ProjectsCollectionViewModel>();
        services.AddScoped<IUsersCollectionViewModel, UsersCollectionViewModel>();
    }
}
