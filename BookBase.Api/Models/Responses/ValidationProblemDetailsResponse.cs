namespace BookBase.Api.Models.Responses;

public class ValidationProblemDetailsResponse : ProblemDetailsResponse
{
    public required IDictionary<string, string[]> Errors { get; set; }
}