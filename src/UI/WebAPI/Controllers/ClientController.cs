using Contracts.Exceptions.NotFound;
using Contracts.Repositories;

using Domain.Filters;
using Domain.Models;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientController : ControllerBase
{
    private readonly IClientRepository _clientRepository;

    public ClientController(IClientRepository clientRepository) => _clientRepository = clientRepository;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var clients = await _clientRepository.GetAllAsync();
            return Ok(clients);
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
            var client = await _clientRepository.GetByIdAsync(id);
            return Ok(client);
        }
        catch (ModelNotFoundException<Client>)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmailAsync(string email)
    {
        try
        {
            ClientFilters filters = new() { Email = email };

            var client = await _clientRepository.GetByFiltersAsync(filters);
            return Ok(client);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromBody] Client client)
    {
        try
        {
            var insertedId = await _clientRepository.InsertAsync(client);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = insertedId }, client);
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
            await _clientRepository.RemoveAsync(id);
            return NoContent();
        }
        catch (ModelNotFoundException<Client>)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] Client client)
    {
        if (id != client.Id)
            return BadRequest();

        try
        {
            await _clientRepository.UpdateAsync(client);
            return NoContent();
        }
        catch (ModelNotFoundException<Client>)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
