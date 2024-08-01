﻿using Microsoft.AspNetCore.Mvc;
using Movies.Api.Mapping;
using Movies.Application.Models;
using Movies.Application.Repsoitory;
using Movies.Contracts.Requests;

namespace Movies.Api.Controllers;

[ApiController]

public class MovieController : ControllerBase
{
    private readonly IMovieRepository _movieRepository;

    public MovieController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpPost(ApiEndPoints.Movies.Create)]
    public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
    {
        var movie = request.MapToMovie();
        await _movieRepository.CreateAsync(movie);
        return Created($"/{ApiEndPoints.Movies.Create}/{movie.Id}", movie);
        //return Ok(movie);
    }

}