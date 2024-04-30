using Atoms.Discoveries.Database.API.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Database.API.Data.DTO;
using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;

namespace ProjectManagement.Database.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
	private readonly DatabaseContext _context;
	private readonly IMapper _mapper;

	public CommentsController(DatabaseContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	// GET: api/Comments
	[HttpGet]
	public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments([FromQuery] PaginationParameters paginationParameters)
	{
		return await _context.Comments
			.Skip((int)paginationParameters.Offset)
			.Take((int)paginationParameters.Limit)
			.Select(comment => new CommentDTO
			{
				Id = comment.Id,
				UserId = comment.UserId,
				TicketId = comment.TicketId,
				Text = comment.Text,
				CreatedAt = comment.CreatedAt
			})
			.OrderBy(comment => comment.CreatedAt)
			.AsNoTracking()
			.ToListAsync();
	}

	// GET: api/Comments/{id}
	[HttpGet("{id}")]
	public async Task<ActionResult<CommentDTO>> GetComment(uint id)
	{
		var comment = await _context.Comments.FindAsync(id);

		if(comment == null)
		{
			return NotFound();
		}

		return _mapper.Map<CommentDTO>(comment);
	}

	// PUT: api/Comments/{id}
	[HttpPut("{id}")]
	public async Task<IActionResult> PutComment(CommentDTO commentDTO)
	{
		if(!IsDTOValid(commentDTO))
		{
			return BadRequest();
		}
		if(!CommentExists(commentDTO.Id))
		{
			return NotFound();
		}

		var comment = _mapper.Map<Comment>(commentDTO);
		_context.Entry(comment).State = EntityState.Modified;

		await _context.SaveChangesAsync();

		return Ok();
	}

	// POST: api/Comments
	[HttpPost]
	public async Task<ActionResult<Comment>> PostComment(CommentDTO commentDTO)
	{
		if(!IsDTOValid(commentDTO))
		{
			return BadRequest();
		}

		var comment = _mapper.Map<Comment>(commentDTO);
		_context.Comments.Add(comment);

		await _context.SaveChangesAsync();

		var result = _mapper.Map<CommentDTO>(comment);
		return CreatedAtAction(nameof(GetComment), new { id = result.Id }, result);
	}

	// DELETE: api/Comments/{id}
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteComment(uint id)
	{
		var comment = await _context.Comments.FindAsync(id);
		if(comment == null)
		{
			return NotFound();
		}

		_context.Comments.Remove(comment);
		await _context.SaveChangesAsync();

		return Ok();
	}

	private bool CommentExists(uint id)
	{
		if(id == 0)
			return false;

		return _context.Comments.Any(e => e.Id == id);
	}

	private bool IsDTOValid(CommentDTO commentDTO)
	{
		if(commentDTO == null)
			return false;

		if(commentDTO.UserId == 0)
			return false;

		if(commentDTO.TicketId == 0)
			return false;

		if(string.IsNullOrWhiteSpace(commentDTO.Text))
			return false;

		return true;
	}
}