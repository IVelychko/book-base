using Microsoft.AspNetCore.Mvc;

namespace BookBase.Api.Models.Requests;

public record DeleteUserRequest([FromRoute] string Id);