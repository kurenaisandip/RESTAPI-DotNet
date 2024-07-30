namespace Movies.Contracts.Responses;

public class MovieResponse
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }

    public required int YearofRelease { get; init; }
    
    //an array of strings with default value of empty string
    public required IEnumerable<string> Genre { get; init; } = Enumerable.Empty<string>();
}