using Microsoft.IdentityModel.Logging;
using ProjectManagement.Database.API.Data.Helpers;
using ProjectManagement.Database.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
	builder.Configuration
		.AddUserSecrets<Program>();
	IdentityModelEventSource.ShowPII = true;
}

// Add services to the container.
var connectionString = builder.Configuration
	.GetConnectionString("DatabaseConnection") ?? throw new ArgumentNullException("Connection string");
builder.Services.AddInfrastructure(connectionString, builder.Environment.IsDevelopment());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AutoMappingProfiles).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
