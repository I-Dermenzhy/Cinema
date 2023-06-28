using Contracts.Exceptions.NotFound;
using Contracts.Repositories;

using Domain.Filters;
using Domain.Models;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketController : ControllerBase
{
    private readonly ITicketRepository _ticketRepository;

    public TicketController(ITicketRepository ticketRepository) =>
        _ticketRepository = ticketRepository;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var tickets = await _ticketRepository.GetAllAsync();
            return Ok(tickets);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("client/{clientId}")]
    public async Task<IActionResult> GetByClientAsync(Guid clientId)
    {
        try
        {
            TicketFilters filters = new() { ClientId = clientId };

            var tickets = await _ticketRepository.GetByFiltersAsync(filters);
            return Ok(tickets);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        try
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            return Ok(ticket);
        }
        catch (ModelNotFoundException<Ticket>)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromBody] Ticket ticket)
    {
        try
        {
            var insertedId = await _ticketRepository.InsertAsync(ticket);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = insertedId }, ticket);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        try
        {
            await _ticketRepository.RemoveAsync(id);
            return NoContent();
        }
        catch (ModelNotFoundException<Ticket>)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] Ticket ticket)
    {
        if (id != ticket.Id)
            return BadRequest();

        try
        {
            await _ticketRepository.UpdateAsync(ticket);
            return NoContent();
        }
        catch (ModelNotFoundException<Ticket>)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
