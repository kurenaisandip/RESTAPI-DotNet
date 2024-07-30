namespace Movies.Contracts.Requests;

public class CreateMovieRequest
{
    public required string Title { get; init; }

    public required int YearofRelease { get; init; }
    
    //an array of strings with default value of empty string
    public required IEnumerable<string> Genre { get; init; } = Enumerable.Empty<string>();
}