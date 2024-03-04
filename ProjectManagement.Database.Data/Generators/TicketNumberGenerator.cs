using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using ProjectManagement.Database.Domain.Entities;

namespace ProjectManagement.Database.Data.Generators;

public class TicketNumberGenerator : ValueGenerator<string>
{
	public override bool GeneratesTemporaryValues => false;

	public override string Next(EntityEntry entry)
	{
		var projectId = (uint?)entry.Property(nameof(Ticket.ProjectId)).CurrentValue;

		if (projectId == null) throw new ArgumentNullException(nameof(Ticket.ProjectId));

		if (entry.Context is not DatabaseContext context) throw new ArgumentException("Wrong context type", nameof(entry.Context));

		var project = context.Projects.FirstOrDefault(p => p.Id == projectId);

		if (project == null) throw new ArgumentException("No projects with such id", nameof(Ticket.ProjectId));


		var projectAbbreviation = project.Abbreviation;

		var number = project.Sequence++;

		return $"{projectAbbreviation}-{number}";
	}
}