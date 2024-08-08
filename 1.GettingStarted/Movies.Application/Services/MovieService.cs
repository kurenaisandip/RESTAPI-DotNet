using Movies.Application.Models;
using Movies.Application.Repositories;

namespace Movies.Application.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _repository;

    //inject the movie repository from the constructor
    public MovieService(IMovieRepository repository)
    {
        _repository = repository;
    }


    public Task<bool> CreateAsync(Movie movie)
    {
       return _repository.CreateAsync(movie);
    }

    public Task<Movie?> GetByIdAsync(Guid id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<Movie?> GetBySlug(string slug)
    {
       return _repository.GetBySlug(slug);
    }

    public Task<IEnumerable<Movie>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public async Task<Movie?> UpdateAsync(Movie movie)
    {
       var movieExists = await _repository.ExistsByIdAsync(movie.Id);

       if (!movieExists)
       {
           return null;
       }

       await _repository.UpdateAsync(movie);
       return movie;
    }

    public Task<bool> DeleteByIdAsync(Guid id)
    {
        return _repository.DeleteByIdAsync(id);
    }
}