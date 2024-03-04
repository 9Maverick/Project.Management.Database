using Microsoft.EntityFrameworkCore;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Shared.Kernel.Configuration;

namespace ProjectManagement.Database.Data;

public class DatabaseContext : DbContext
{
	private readonly IModelConfiguration _modelConfiguration;

	public DbSet<Comment> Comments => Set<Comment>();
	public DbSet<Project> Projects => Set<Project>();
	public DbSet<Team> Teams => Set<Team>();
	public DbSet<Ticket> Tickets => Set<Ticket>();
	public DbSet<User> Users => Set<User>();


	public DatabaseContext(DbContextOptions<DatabaseContext> options, IModelConfiguration modelConfiguration) :
		base(options)
	{
		_modelConfiguration = modelConfiguration;
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
		_modelConfiguration.ConfigureModel(modelBuilder);
	}
}