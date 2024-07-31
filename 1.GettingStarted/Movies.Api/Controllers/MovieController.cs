﻿using Microsoft.AspNetCore.Mvc;
using Movies.Application.Models;
using Movies.Application.Repsoitory;
using Movies.Contracts.Requests;

namespace Movies.Api.Controllers;

[ApiController]
[Route("api")]
public class MovieController : ControllerBase
{
    private readonly IMovieRepository _movieRepository;

    public MovieController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpPost("movies")]
    public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
    {
        var movie = new Movie
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            YearOfRelease = request.YearofRelease,
            Genres = request.Genre.ToList()
        };
        await _movieRepository.CreateAsync(movie);
        // return Created($"/api/movies/{movie.Id}", movie);
        return Ok(movie);
    }

}