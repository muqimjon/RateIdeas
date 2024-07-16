global using EcoLink.WebApi.Controllers.Commons;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;

namespace EcoLink.WebApi.Controllers.Commons;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
}
