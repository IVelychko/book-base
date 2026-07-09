using Microsoft.AspNetCore.Mvc;

namespace BookBase.Api.Models.Requests;

public record GetUserByIdRequest([FromRoute] Guid Id);