using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Collections;
using ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Page;

namespace ProjectManagement.Database.Panel.Configuration;
public static class ServicesConfiguration
{
    public static void AddViewModels(this IServiceCollection services)
    {
        AddCollectionsViewModels(services);

        AddPagesViewModels(services);
    }

    public static void AddCollectionsViewModels(this IServiceCollection services)
    {
        services.AddTransient<IEntityCollectionViewModel<IProject>, ProjectsCollectionViewModel>();
        services.AddTransient<IEntityCollectionViewModel<IUser>, UsersCollectionViewModel>();
        services.AddTransient<IChildEntityCollectionViewModel<ITeam>, TeamsCollectionViewModel>();
    }

    public static void AddPagesViewModels(this IServiceCollection services)
    {
        services.AddTransient<IEditableEntityViewModel<IUser>, UserPageViewModel>();
        services.AddTransient<IEditableEntityViewModel<IProject>, ProjectPageViewModel>();
        services.AddTransient<IEditableChildEntityViewModel<ITeam>, TeamPageViewModel>();
    }
}
