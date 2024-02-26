using Microsoft.EntityFrameworkCore;
using Project.Management.Database.Domain;
using Project.Management.Database.Shared.Kernel.Configuration;

namespace Project.Management.Database.Data;

public class DatabaseContext : DbContext
{
	private readonly IModelConfiguration _modelConfiguration;

	public DbSet<Comment> Comments => Set<Comment>();
	public DbSet<Domain.Project> Projects => Set<Domain.Project>();
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