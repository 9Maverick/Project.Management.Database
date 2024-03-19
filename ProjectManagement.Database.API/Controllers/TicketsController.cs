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
public class TicketsController : ControllerBase
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public TicketsController(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/Tickets
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketDTO>>> GetTickets([FromQuery] PaginationParameters paginationParameters)
    {
        return await _context.Tickets
            .Skip((int)paginationParameters.Offset)
            .Take((int)paginationParameters.Limit)
            .Select(ticket => new TicketDTO
            {
                Id = ticket.Id,
                Number = ticket.Number,
                ProjectId = ticket.ProjectId,
                CreatorId = ticket.CreatorId,
                AssigneeId = ticket.AssigneeId,
                ParentId = ticket.ParentId,
                Type = ticket.Type.ToString(),

            })
            .AsNoTracking()
            .ToListAsync();
    }

    // GET: api/Tickets/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<TicketDTO>> GetTicket(uint id)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket == null)
        {
            return NotFound();
        }

        return _mapper.Map<TicketDTO>(ticket);
    }

    // PUT: api/Tickets/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTicket(TicketDTO ticketDTO)
    {
        if (!IsDTOValid(ticketDTO))
        {
            return BadRequest();
        }
        if (!TicketExists(ticketDTO.Id))
        {
            return NotFound();
        }

        var ticket = _mapper.Map<Ticket>(ticketDTO);
        _context.Entry(ticket).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return Ok();
    }

    // POST: api/Tickets
    [HttpPost]
    public async Task<ActionResult<Ticket>> PostTicket(TicketDTO ticketDTO)
    {
        if (!IsDTOValid(ticketDTO))
        {
            return BadRequest();
        }

        var ticket = _mapper.Map<Ticket>(ticketDTO);

        ticket.Number = null;

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<TicketDTO>(ticket);

        return CreatedAtAction(nameof(GetTicket), new { id = result.Id }, result);
    }

    // DELETE: api/Tickets/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(uint id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }

        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();

        return Ok();
    }

    private bool TicketExists(uint id)
    {
        if (id == 0) return false;

        return _context.Tickets.Any(e => e.Id == id);
    }

    private bool IsDTOValid(TicketDTO ticketDTO)
    {
        if (ticketDTO == null) return false;

        if (ticketDTO.ProjectId == 0) return false;

        if (ticketDTO.CreatorId == 0) return false;

        if (string.IsNullOrWhiteSpace(ticketDTO.Title)) return false;

        return true;
    }
}