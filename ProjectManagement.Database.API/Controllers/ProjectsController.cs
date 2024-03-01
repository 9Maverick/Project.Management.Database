using Atoms.Discoveries.Database.API.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Database.API.Data.DTO;
using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain;

namespace ProjectManagement.Database.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
	private readonly DatabaseContext _context;
	private readonly IMapper _mapper;

	public ProjectsController(DatabaseContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	// GET: api/Projects
	[HttpGet]
	public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects([FromQuery] PaginationParameters paginationParameters)
	{
		return await _context.Projects
			.Skip((int)paginationParameters.Offset)
			.Take((int)paginationParameters.Limit)
			.Select(project => new ProjectDTO
			{
				Id = project.Id,
				Abbreviation = project.Abbreviation,
				Name = project.Name,
			})
			.OrderBy(project => project.Name)
			.AsNoTracking()
			.ToListAsync();

	}

	// GET: api/Projects/{id}
	[HttpGet("{id}")]
	public async Task<ActionResult<ProjectDTO>> GetProject(uint id)
	{
		var project = await _context.Projects.FindAsync(id);

		if (project == null)
		{
			return NotFound();
		}

		return _mapper.Map<ProjectDTO>(project);
	}

	// PUT: api/Projects/{id}
	[HttpPut("{id}")]
	public async Task<IActionResult> PutProject(ProjectDTO projectDTO)
	{
		if (!IsDTOValid(projectDTO))
		{
			return BadRequest();
		}
		if (!ProjectExists(projectDTO.Id))
		{
			return NotFound();
		}

		var project = _mapper.Map<Project>(projectDTO);
		_context.Entry(project).State = EntityState.Modified;

		await _context.SaveChangesAsync();

		return Ok();
	}

	// POST: api/Projects
	[HttpPost]
	public async Task<ActionResult<Project>> PostProject(ProjectDTO projectDTO)
	{
		if (!IsDTOValid(projectDTO))
		{
			return BadRequest();
		}

		var project = _mapper.Map<Project>(projectDTO);
		_context.Projects.Add(project);

		await _context.SaveChangesAsync();

		var result = _mapper.Map<ProjectDTO>(project);
		return CreatedAtAction(nameof(GetProject), new { id = result.Id }, result);
	}

	// DELETE: api/Projects/{id}
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteProject(uint id)
	{
		var project = await _context.Projects.FindAsync(id);
		if (project == null)
		{
			return NotFound();
		}

		_context.Projects.Remove(project);
		await _context.SaveChangesAsync();

		return Ok();
	}

	private bool ProjectExists(uint id)
	{
		if (id == 0) return false;

		return _context.Projects.Any(e => e.Id == id);
	}

	private bool IsDTOValid(ProjectDTO projectDTO)
	{
		if (projectDTO == null) return false;

		if (string.IsNullOrWhiteSpace(projectDTO.Abbreviation)) return false;

		if (string.IsNullOrWhiteSpace(projectDTO.Name)) return false;

		return true;
	}
}
