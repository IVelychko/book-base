using Microsoft.AspNetCore.Mvc;

namespace BookBase.Api.Models.Requests;

public record GetBookByIdRequest([FromRoute] Guid Id);