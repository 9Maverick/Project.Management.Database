using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Database.API.Data.DTO;
using ProjectManagement.Database.API.Data.Models;
using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;

namespace ProjectManagement.Database.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamsController : ControllerBase
{
	private readonly DatabaseContext _context;
	private readonly IMapper _mapper;

	public TeamsController(DatabaseContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	// GET: api/Teams
	[HttpGet]
	public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeams([FromQuery] PaginationParameters paginationParameters)
	{
		return await _context.Teams
			.Skip((int)paginationParameters.Offset)
			.Take((int)paginationParameters.Limit)
			.Select(team => new TeamDTO
			{
				Id = team.Id,
				Name = team.Name,
				ParentId = team.ParentId
			})
			.OrderBy(team => team.Name)
			.AsNoTracking()
			.ToListAsync();
	}

	// GET: api/Teams/{id}
	[HttpGet("{id}")]
	public async Task<ActionResult<TeamDTO>> GetTeam(uint id)
	{
		var team = await _context.Teams.FindAsync(id);

		if(team == null)
		{
			return NotFound();
		}

		return _mapper.Map<TeamDTO>(team);
	}

	// PUT: api/Teams/{id}
	[HttpPut("{id}")]
	public async Task<IActionResult> PutTeam(TeamDTO teamDTO)
	{
		if(!IsDTOValid(teamDTO))
		{
			BadRequest();
		}
		if(!TeamExists(teamDTO.Id))
		{
			return NotFound();
		}

		var team = _mapper.Map<Team>(teamDTO);

		_context.Entry(team).State = EntityState.Modified;
		await _context.SaveChangesAsync();

		return Ok();
	}

	// POST: api/Teams
	[HttpPost]
	public async Task<ActionResult<Team>> PostTeam(TeamDTO teamDTO)
	{
		if(!IsDTOValid(teamDTO))
		{
			BadRequest();
		}

		var team = _mapper.Map<Team>(teamDTO);

		_context.Teams.Add(team);
		await _context.SaveChangesAsync();

		var result = _mapper.Map<TeamDTO>(team);
		return CreatedAtAction(nameof(GetTeam), new { id = result.Id }, result);
	}

	// DELETE: api/Teams/{id}
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteTeam(uint id)
	{
		var team = await _context.Teams.FindAsync(id);
		if(team == null)
		{
			return NotFound();
		}

		_context.Teams.Remove(team);
		await _context.SaveChangesAsync();

		return Ok();
	}

	private bool TeamExists(uint id)
	{
		if(id == 0)
			return false;

		return _context.Teams.Any(e => e.Id == id);
	}

	private bool IsDTOValid(TeamDTO teamDTO)
	{
		if(teamDTO == null)
			return false;

		if(string.IsNullOrWhiteSpace(teamDTO.Name))
			return false;

		return true;
	}
}