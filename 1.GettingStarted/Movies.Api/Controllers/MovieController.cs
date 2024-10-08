﻿using Microsoft.AspNetCore.Mvc;
using Movies.Api.Mapping;
using Movies.Application.Repositories;
using Movies.Application.Services;
using Movies.Contracts.Requests;

namespace Movies.Api.Controllers;

[ApiController]

public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpPost(ApiEndPoints.Movies.Create)]
    public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
    {
        var movie = request.MapToMovie();
        await _movieService.CreateAsync(movie);
        return CreatedAtAction(nameof(Get), new {idOrSlug = movie.Id}, movie);
        // return Created($"/{ApiEndPoints.Movies.Create}/{movie.Id}", movie);
        //return Ok(movie);
    }

    [HttpGet(ApiEndPoints.Movies.Get)]
    public async Task<IActionResult> Get([FromRoute] string idOrSlug)
    {
        var movie = Guid.TryParse(idOrSlug, out var id)
            ? await _movieService.GetByIdAsync(id)
            : await _movieService.GetBySlug(idOrSlug);
        if (movie is null)
        {
            return NotFound();
        }

        var response = movie.MapToResponse();
        return Ok(response);
    }
    
    [HttpGet(ApiEndPoints.Movies.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var movies = await _movieService.GetAllAsync();
        var moviesResponse = movies.MapToResponse();
        return Ok(moviesResponse);
    }

    [HttpPut(ApiEndPoints.Movies.Update)]
    public async Task<IActionResult> Update([FromRoute]Guid id,[FromBody]UpdateMovieRequest request)
    {
        //map the movie object to new domain object
        var movie = request.MapToMovie(id);
        var updatedMovie = await _movieService.UpdateAsync(movie);
        if (updatedMovie is null)
        {
            return NotFound();
        }

        var response = updatedMovie.MapToResponse();
        return Ok(response);
    }

    [HttpDelete(ApiEndPoints.Movies.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleted = await _movieService.DeleteByIdAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return Ok();
    }


}