global using EcoLink.WebApi.Controllers.Commons;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace EcoLink.WebApi.Controllers.Commons;

[EnableCors("AllowAll")]
[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
}
