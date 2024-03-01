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
public class UsersController : ControllerBase
{
	private readonly DatabaseContext _context;
	private readonly IMapper _mapper;

	public UsersController(DatabaseContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	// GET: api/Users
	[HttpGet]
	public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers([FromQuery] PaginationParameters paginationParameters)
	{
		return await _context.Users
			.Skip((int)paginationParameters.Offset)
			.Take((int)paginationParameters.Limit)
			.Select(user => new UserDTO
			{
				Id = user.Id,
				Name = user.Name,
			})
			.OrderBy(user => user.Id)
			.AsNoTracking()
			.ToListAsync();
	}

	// GET: api/Users/{id}
	[HttpGet("{id}")]
	public async Task<ActionResult<UserDTO>> GetUser(uint id)
	{
		var user = await _context.Users.FindAsync(id);

		if (user == null)
		{
			return NotFound();
		}

		return _mapper.Map<UserDTO>(user);
	}

	// PUT: api/Users/{id}
	[HttpPut("{id}")]
	public async Task<IActionResult> PutUser(UserDTO userDTO)
	{
		if (!IsDTOValid(userDTO))
		{
			return BadRequest();
		}
		if (!UserExists(userDTO.Id))
		{
			return NotFound();
		}

		var user = _mapper.Map<User>(userDTO);

		_context.Entry(user).State = EntityState.Modified;
		await _context.SaveChangesAsync();

		return Ok();
	}

	// POST: api/Users
	[HttpPost]
	public async Task<ActionResult<User>> PostUser(UserDTO userDTO)
	{
		if (!IsDTOValid(userDTO))
		{
			return BadRequest();
		}

		var user = _mapper.Map<User>(userDTO);

		_context.Users.Add(user);
		await _context.SaveChangesAsync();

		var result = _mapper.Map<UserDTO>(user);
		return CreatedAtAction(nameof(GetUser), new { id = result.Id }, result);
	}

	// DELETE: api/Users/{id}
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteUser(uint id)
	{
		var user = await _context.Users.FindAsync(id);
		if (user == null)
		{
			return NotFound();
		}

		_context.Users.Remove(user);
		await _context.SaveChangesAsync();

		return Ok();
	}

	private bool UserExists(uint id)
	{
		if (id == 0) return false;

		return _context.Users.Any(e => e.Id == id);
	}

	private bool IsDTOValid(UserDTO userInDTO)
	{
		if (userInDTO == null) return false;

		if (string.IsNullOrWhiteSpace(userInDTO.Name)) return false;

		return true;
	}
}
