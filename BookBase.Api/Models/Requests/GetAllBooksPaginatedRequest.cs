using Microsoft.AspNetCore.Mvc;

namespace BookBase.Api.Models.Requests;

public class GetAllBooksPaginatedRequest
{
    [FromQuery]
    public int PageNumber { get; set; } = 1;

    [FromQuery]
    public int PageSize { get; set; } = 12;
}