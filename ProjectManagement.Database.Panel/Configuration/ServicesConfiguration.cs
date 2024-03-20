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
        services.AddTransient<IEntityCollectionViewModel<IProject>, ProjectCollectionViewModel>();
        services.AddTransient<IEntityCollectionViewModel<IUser>, UserCollectionViewModel>();
        services.AddTransient<IEntityCollectionViewModel<ITeam>, TeamCollectionViewModel>();
        services.AddTransient<INestedEntityCollectionViewModel<ITicket, IProject>, TicketCollectionViewModel>();
    }

    public static void AddPagesViewModels(this IServiceCollection services)
    {
        services.AddTransient<IUserPageViewModel, UserPageViewModel>();
        services.AddTransient<IProjectPageViewModel, ProjectPageViewModel>();
        services.AddTransient<ITeamPageViewModel, TeamPageViewModel>();
        services.AddTransient<ITicketPageViewModel, TicketPageViewModel>();
    }
}
