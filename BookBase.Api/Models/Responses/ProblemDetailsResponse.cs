namespace BookBase.Api.Models.Responses;

public class ProblemDetailsResponse
{
    public required int StatusCode { get; set; }

    public required string Title { get; set; }
}