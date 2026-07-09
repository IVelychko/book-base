using Microsoft.AspNetCore.Mvc;

namespace BookBase.Api.Models.Requests;

public record DeleteBookRequest([FromRoute] string Id);