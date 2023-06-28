using Contracts.Exceptions.NotFound;
using Contracts.Repositories;

using Domain.Filters;
using Domain.Models;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/sessions")]
public class SessionController : ControllerBase
{
    private readonly ISessionRepository _sessionRepository;

    public SessionController(ISessionRepository sessionRepository) =>
        _sessionRepository = sessionRepository;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var sessions = await _sessionRepository.GetAllAsync();
            return Ok(sessions);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("date/{date}")]
    public async Task<IActionResult> GetByDateAsync(DateOnly date)
    {
        try
        {
            SessionFilters filters = new() { Date = date };

            var sessions = await _sessionRepository.GetByFiltersAsync(filters);
            return Ok(sessions);
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
            var session = await _sessionRepository.GetByIdAsync(id);
            return Ok(session);
        }
        catch (ModelNotFoundException<Session>)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromBody] Session session)
    {
        try
        {
            var insertedId = await _sessionRepository.InsertAsync(session);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = insertedId }, session);
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
            await _sessionRepository.RemoveAsync(id);
            return NoContent();
        }
        catch (ModelNotFoundException<Session>)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] Session session)
    {
        if (id != session.Id)
            return BadRequest();

        try
        {
            await _sessionRepository.UpdateAsync(session);
            return NoContent();
        }
        catch (ModelNotFoundException<Session>)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}