using Microsoft.IdentityModel.Logging;
using ProjectManagement.Database.Infrastructure;
using ProjectManagement.Database.Panel.Components;
using ProjectManagement.Database.Panel.Configuration;
using Syncfusion.Blazor;

namespace ProjectManagement.Database.Panel;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		if(builder.Environment.IsDevelopment())
		{
			builder.Configuration
				.AddUserSecrets<Program>();
			IdentityModelEventSource.ShowPII = true;
		}

		// Add services to the container.
		var connectionString = builder.Configuration
			.GetConnectionString("DatabaseConnection") ?? throw new ArgumentNullException("Connection string");
		builder.Services.AddInfrastructure(connectionString, builder.Environment.IsDevelopment());

		builder.Services.AddViewModels();

		builder.Services.AddSyncfusionBlazor();

		builder.Services.AddRazorComponents()
			.AddInteractiveServerComponents();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if(!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();

		app.UseStaticFiles();
		app.UseAntiforgery();

		app.MapRazorComponents<App>()
			.AddInteractiveServerRenderMode();

		app.Run();
	}
}