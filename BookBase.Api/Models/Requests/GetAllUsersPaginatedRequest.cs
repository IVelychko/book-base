using Microsoft.AspNetCore.Mvc;

namespace BookBase.Api.Models.Requests;

public class GetAllUsersPaginatedRequest
{
    [FromQuery]
    public int PageNumber { get; set; } = 1;

    [FromQuery]
    public int PageSize { get; set; } = 12;
}