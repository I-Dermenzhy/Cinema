using Contracts.Exceptions.NotFound;
using Contracts.Repositories;

using Domain.Filters;
using Domain.Models;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/seats")]
public class SeatController : ControllerBase
{
    private readonly ISeatRepository _seatRepository;

    public SeatController(ISeatRepository seatRepository) =>
        _seatRepository = seatRepository;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var seats = await _seatRepository.GetAllAsync();
            return Ok(seats);
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
            var seat = await _seatRepository.GetByIdAsync(id);
            return Ok(seat);
        }
        catch (ModelNotFoundException<Seat>)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetByCategoryAsync(SeatCategory category)
    {
        try
        {
            SeatFilters filters = new() { Category = category };

            var seats = await _seatRepository.GetByFiltersAsync(filters);
            return Ok(seats);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("location/{row}/{place}")]
    public async Task<IActionResult> GetByLocationAsync(int row, int place)
    {
        try
        {
            SeatFilters filters = new() { Row = row, Place = place };

            var seat = await _seatRepository.GetByFiltersAsync(filters);
            return Ok(seat);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromBody] Seat seat)
    {
        try
        {
            var insertedId = await _seatRepository.InsertAsync(seat);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = insertedId }, seat);
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
            await _seatRepository.RemoveAsync(id);
            return NoContent();
        }
        catch (ModelNotFoundException<Seat>)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] Seat seat)
    {
        if (id != seat.Id)
            return BadRequest();

        try
        {
            await _seatRepository.UpdateAsync(seat);
            return NoContent();
        }
        catch (ModelNotFoundException<Seat>)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
