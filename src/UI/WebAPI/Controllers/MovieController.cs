using Contracts.Exceptions.NotFound;
using Contracts.Repositories;

using Domain.Filters;
using Domain.Models;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/movies")]
public class MovieController : ControllerBase
{
    private readonly IMovieRepository _movieRepository;

    public MovieController(IMovieRepository movieRepository) =>
        _movieRepository = movieRepository;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var movies = await _movieRepository.GetAllAsync();
            return Ok(movies);
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
            var movie = await _movieRepository.GetByIdAsync(id);
            return Ok(movie);
        }
        catch (ModelNotFoundException<Movie>)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("genre/{genre}")]
    public async Task<IActionResult> GetByGenreAsync(string genre)
    {
        try
        {
            MovieFilters filters = new() { Genre = genre };

            var movies = await _movieRepository.GetByFiltersAsync(filters);
            return Ok(movies);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("producer/{producer}")]
    public async Task<IActionResult> GetByProducerAsync(string producer)
    {
        try
        {
            MovieFilters filters = new() { Producer = producer };

            var movies = await _movieRepository.GetByFiltersAsync(filters);
            return Ok(movies);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("title/{title}")]
    public async Task<IActionResult> GetByTitleAsync(string title)
    {
        try
        {
            MovieFilters filters = new() { Title = title };

            var movie = await _movieRepository.GetByFiltersAsync(filters);
            return Ok(movie);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromBody] Movie movie)
    {
        try
        {
            var insertedId = await _movieRepository.InsertAsync(movie);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = insertedId }, movie);
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
            await _movieRepository.RemoveAsync(id);
            return NoContent();
        }
        catch (ModelNotFoundException<Movie>)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] Movie movie)
    {
        if (id != movie.Id)
            return BadRequest();

        try
        {
            await _movieRepository.UpdateAsync(movie);
            return NoContent();
        }
        catch (ModelNotFoundException<Movie>)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
