global using MediatR;
global using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace RateIdeas.WebApi.Controllers.Commons;

[EnableCors("AllowAll")]
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BaseController : ControllerBase
{
}
